using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.Shared;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.UnitTests;

public class VolunteerRequestTests
{
    public static VolunteerRequest CreateVolunteerRequest()
    {
        var adminId = AdminId.NewAdminId();
        var userId = UserId.NewUserId();

        var fullName = FullName.Create("FiresName", "SecondName", "SurName").Value;
        var email = Email.Create("test@mail.ru").Value;
        var description = DescriptionValueObject.Create("Some test description").Value;
        var phone = Phone.Create("+70951234556").Value;
        var volunteerInfo = new VolunteerInfo(fullName, email, description, phone);

        var status = RequestStatus.None;
        var createAt = DateTime.UtcNow;        
        
        return VolunteerRequest.Create(
            adminId,
            userId,
            volunteerInfo,
            status,
            createAt);
    }

    [Fact]
    public void GetOnReview_ItShould_Set_Status_To_Submitted()
    {
        //Arrange
        var volunteerRequest = CreateVolunteerRequest();

        //Act
        volunteerRequest.GetOnReview();

        //Assert
        Assert.Equal(RequestStatus.Submitted, volunteerRequest.Status);
    }

    [Fact]
    public void SendToRevision_ItShould_Set_Comment_DisscusionId_Status_To_Reversion_required()
    {
        //Arrange
        var volunteerRequest = CreateVolunteerRequest();
        var rejectionComment = Comment.Create("Rejected test comment").Value;
        var disscucionId = DisscusionId.NewDisscusionId();

        //Act
        volunteerRequest.SendToRevision(rejectionComment, disscucionId);

        //Assert
        Assert.Equal(RequestStatus.Reversion_required, volunteerRequest.Status);
        Assert.Equal(volunteerRequest.RejectionComment, rejectionComment);
        Assert.Equal(volunteerRequest.DisscusionId, disscucionId);
    }

    [Fact]
    public void RejectRequest_ItShould_Set_Status_To_Rejected()
    {
        //Arrange
        var volunteerRequest = CreateVolunteerRequest();

        //Act
        volunteerRequest.RejectRequest();

        //Assert
        Assert.Equal(RequestStatus.Rejected, volunteerRequest.Status);
    }

    [Fact]
    public void ApproveRequest_ItShould_Set_Status_To_Approved()
    {
        //Arrange
        var volunteerRequest = CreateVolunteerRequest();

        //Act
        volunteerRequest.ApproveRequest();

        //Assert
        Assert.Equal(RequestStatus.Approved, volunteerRequest.Status);
    }
}