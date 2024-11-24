using PetHome.Accounts.Domain;
using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.UnitTests;

public class DisscusionTests
{
    public static Result<Disscusion> CreateDisscusion_2users()
    {
        var disscusionId = DisscusionId.NewDisscusionId();
        var relationId = Guid.NewGuid();
        var users = new List<User>()
        {
            User.CreateParticipant("Username1", "test1@mail.ru", new Role()),
            User.CreateParticipant("Username2", "test2@mail.ru", new Role())
        };
        var disscusion = Disscusion.Create(disscusionId, relationId, users);

        return disscusion;
    }

    public static Result<Disscusion> CreateDisscusion_1user()
    {
        var disscusionId = DisscusionId.NewDisscusionId();
        var relationId = Guid.NewGuid();
        var users = new List<User>()
        {
            User.CreateParticipant("Username1", "test1@mail.ru", new Role())
        };
        var disscusion = Disscusion.Create(disscusionId, relationId, users);

        return disscusion;
    }

    [Fact]
    public void Create_Disscusion_ItShould_Return_Disscusion_If_All_Parameters_Are_Valid()
    {
        //Arrange

        //Act
        var result = CreateDisscusion_2users();

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Create_Disscusion_ItShould_Return_Error_If_Users_Less_Than_2()
    {
        //Arrange 

        //Act
        var result = CreateDisscusion_1user();

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void AddComment_ItShould_Return_Error_If_UserId_Not_Found()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "TestComment";

        //Act
        var result = disscusion.AddComment(text, userId);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void AddComment_ItShould_Return_Error_If_Text_Is_Empty()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = " ";

        //Act
        var result = disscusion.AddComment(text, userId);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void AddComment_ItShould_Return_Success_If_Parameters_Are_Valid()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";

        //Act
        var result = disscusion.AddComment(text, userId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(disscusion.Messages);
    }

    [Fact]
    public void DeleteComment_ItShould_Return_Error_If_UserId_Not_Found()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = MessageId.NewMessageId();

        //Act
        var result = disscusion.DeleteComment(userId, messageId);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void DeleteComment_ItShould_Return_Error_If_MessageId_Not_Found()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = MessageId.NewMessageId();

        //Act
        var result = disscusion.DeleteComment(userId, messageId);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void DeleteComment_ItShould_Return_Success_If_Parameters_Are_Valid()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = disscusion.Messages.First().MessageId;

        //Act
        var result = disscusion.DeleteComment(userId, messageId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(disscusion.Messages);
    }

    [Fact]
    public void EditComment_ItShould_Return_Error_If_UserId_Not_Found()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = MessageId.NewMessageId();
        var newText = "New test comment";

        //Act
        var result = disscusion.EditComment(userId, messageId, newText);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void EditComment_ItShould_Return_Error_If_MessageId_Not_Found()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = MessageId.NewMessageId();
        var newText = "New test comment";

        //Act
        var result = disscusion.EditComment(userId, messageId, newText);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void EditComment_ItShould_Return_Error_If_New_Text_Is_Empty()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = disscusion.Messages.First().MessageId;
        var newText = " ";

        //Act
        var result = disscusion.EditComment(userId, messageId, newText);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void EditComment_ItShould_Return_Success_If_Parameters_Are_Valid()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);
        var messageId = disscusion.Messages.First().MessageId;
        var newText = "New test comment";

        //Act
        var result = disscusion.EditComment(userId, messageId, newText);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.True(disscusion.Messages.First().Text == newText);
    }

    [Fact]
    public void CloseDisscusion_ItShould_Set_Disscusion_State_Closed()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First().Id;
        var text = "Test comments";
        disscusion.AddComment(text, userId);       

        //Act
        disscusion.CloseDisscusion();

        //Assert
        Assert.True(disscusion.State == DisscusionState.Closed);        
    }
}