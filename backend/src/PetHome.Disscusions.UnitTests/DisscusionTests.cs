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
        var users = new List<Guid>()
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };
        var disscusion = Disscusion.Create(disscusionId, relationId, users);

        return disscusion;
    }

    public static Result<Disscusion> CreateDisscusion_1user()
    {
        var disscusionId = DisscusionId.NewDisscusionId();
        var relationId = Guid.NewGuid();
        var users = new List<Guid>()
        {
            Guid.NewGuid()
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
    public void AddComment_ItShould_Return_MessageGuid()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = disscusion.Users.First();
        var text = "Test comments";
        var createAt = DateTime.Now;
        var messageId = MessageId.NewMessageId();

        var message = Message.Create(messageId, text, createAt, false, userId);
        //Act

        var result = disscusion.AddComment(message.Value);

        //Assert
        Assert.IsType<Guid>(result);        
    }

    [Fact]
    public void DeleteComment_ItShould_Return_DeletedMessageId()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "Test comments";
        var createAt = DateTime.Now;
        var messageId = MessageId.NewMessageId();

        var message = Message.Create(messageId, text, createAt, false, userId);
        var commentId = disscusion.AddComment(message.Value);

        //Act
        var result = disscusion.DeleteComment(message.Value);

        //Assert
        Assert.IsType<Guid>(result);       
    } 
   
    [Fact]
    public void EditComment_ItShould_Change_Text_In_Message()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "Test comments";
        var createAt = DateTime.Now;
        var messageId = MessageId.NewMessageId();

        var message = Message.Create(messageId, text, createAt, false, userId);
        disscusion.AddComment(message.Value);
        
        var newText = "New test comment";

        //Act
        message.Value.Edit(newText);

        //Assert
        Assert.Contains(newText, message.Value.Text);        
    }

    [Fact]
    public void EditComment_ItShould_Set_Message_IsEdit_True()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;
        var userId = Guid.NewGuid();
        var text = "Test comments";
        var createAt = DateTime.Now;
        var messageId = MessageId.NewMessageId();

        var message = Message.Create(messageId, text, createAt, false, userId);
        disscusion.AddComment(message.Value);

        var newText = "New test comment";

        //Act
        message.Value.Edit(newText);

        //Assert
        Assert.True(message.Value.IsEdited);
    }

    [Fact]
    public void CloseDisscusion_ItShould_Set_Disscusion_State_Closed()
    {
        //Arrange 
        var disscusion = CreateDisscusion_2users().Value;           

        //Act
        disscusion.CloseDisscusion();

        //Assert
        Assert.True(disscusion.State == DisscusionState.Closed);        
    }
}