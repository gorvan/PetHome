using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using static System.Net.Mime.MediaTypeNames;

namespace PetHome.Disscusions.UnitTests;
public class MessageTests
{
    public static Result<Message> CreateMessage(string text, bool isEdited, Guid userId)
    {       
        var messageId = MessageId.NewMessageId();
        var date = DateTime.UtcNow;
        return Message.Create(messageId, text, date, isEdited, userId);
    }

    [Fact]
    public void Create_ItShould_Return_Message_If_All_Parameters_Are_Valid()
    {
        //Arrange
        var text = "Test message";
        var isEdited = false;
        var userId = Guid.NewGuid();


        //Act
        var result = CreateMessage(text, isEdited, userId);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Create_ItShould_Return_Error_If_Text_Is_Empty()
    {
        //Arrange
        var text = " ";
        var isEdited = false;
        var userId = Guid.NewGuid();


        //Act
        var result = CreateMessage(text, isEdited, userId);

        //Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
    }
}
