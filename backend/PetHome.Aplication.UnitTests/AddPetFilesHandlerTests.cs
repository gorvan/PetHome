using Microsoft.Extensions.Logging;
using Moq;
using PetHome.Application.FileProvider;
using PetHome.Application.Volunteers.Shared;
using PetHome.Application.VolunteersManagement;
using PetHome.Application.VolunteersManagement.PetManagement.AddPetFiles;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.UnitTests
{
    public class AddPetFilesHandlerTests
    {
        [Fact]
        public async Task ItShould_Success_File_Add_To_Pet()
        {
            //Arrange
            var volunteerMockReturn = VolunteerTests.CreateVolunteer();
            var pet = VolunteerTests.CreatePet();
            volunteerMockReturn.AddPet(pet);

            var token = new CancellationTokenSource().Token;

            var volunteerRepsitoryMock = new Mock<IVolunteerRepository>();
            volunteerRepsitoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(volunteerMockReturn, true, Error.None)));

            var fileProviderMock = new Mock<IFileProvider>();
            fileProviderMock.Setup(p => p.UploadFile(It.IsAny<FileData>(), token))
            .Returns(Task.Run(() => new Result<string>("test", true, Error.None)));

            var loggerMock = new Mock<ILogger<AddPetFilesHandler>>();

            var addPetFilesHandler = new AddPetFilesHandler(
                volunteerRepsitoryMock.Object,
                fileProviderMock.Object,
                loggerMock.Object);

            var volunteerId = Guid.NewGuid();
            var petId = pet.Id.Id;
            var name = "test";
            var content = "image/jpeg";
            using var stream = new MemoryStream();
            var files = new FileDto(stream, name, content);
            var addPetFileCommand =
                new AddPetFilesCommand(volunteerId, petId, [files]);

            //Act
            var result = await addPetFilesHandler.Execute(addPetFileCommand, token);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ItShould_Return_NotFound_Error_If_Volunteer_Does_Not_Have_Pet()
        {
            //Arrange
            var volunteerRepsitoryMock = new Mock<IVolunteerRepository>();
            volunteerRepsitoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(VolunteerTests.CreateVolunteer(), true, Error.None)));

            var fileProviderMock = new Mock<IFileProvider>();

            var token = new CancellationTokenSource().Token;
            var loggerMock = new Mock<ILogger<AddPetFilesHandler>>();

            var addPetFilesHandler = new AddPetFilesHandler(
                volunteerRepsitoryMock.Object,
                fileProviderMock.Object,
                loggerMock.Object);

            var volunteerId = Guid.NewGuid();
            var petId = Guid.NewGuid();
            var name = "test";
            var content = "image/jpeg";
            using var stream = new MemoryStream();
            var files = new FileDto(stream, name, content);
            var addPetFileCommand =
                new AddPetFilesCommand(volunteerId, petId, [files]);

            //Act
            var result = await addPetFilesHandler.Execute(addPetFileCommand, token);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task ItShould_Return_Error_If_File_Not_Uploaded()
        {
            //Arrange
            var volunteerMockReturn = VolunteerTests.CreateVolunteer();
            var pet = VolunteerTests.CreatePet();
            volunteerMockReturn.AddPet(pet);

            var token = new CancellationTokenSource().Token;

            var volunteerRepsitoryMock = new Mock<IVolunteerRepository>();
            volunteerRepsitoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(volunteerMockReturn, true, Error.None)));

            var fileProviderMock = new Mock<IFileProvider>();
            fileProviderMock.Setup(p => p.UploadFile(It.IsAny<FileData>(), token))
            .Returns(Task.Run(() => new Result<string>("", false, Error.Failure("test", "test"))));

            var loggerMock = new Mock<ILogger<AddPetFilesHandler>>();

            var addPetFilesHandler = new AddPetFilesHandler(
                volunteerRepsitoryMock.Object,
                fileProviderMock.Object,
                loggerMock.Object);

            var volunteerId = Guid.NewGuid();
            var petId = pet.Id.Id;
            var name = "test";
            var content = "image/jpeg";
            using var stream = new MemoryStream();
            var files = new FileDto(stream, name, content);
            var addPetFileCommand =
                new AddPetFilesCommand(volunteerId, petId, [files]);

            //Act
            var result = await addPetFilesHandler.Execute(addPetFileCommand, token);

            //Assert            
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
        }


        [Fact]
        public async Task ItShould_Set_Photos_To_Pet()
        {
            //Arrange
            var volunteerMockReturn = VolunteerTests.CreateVolunteer();
            var pet = VolunteerTests.CreatePet();
            volunteerMockReturn.AddPet(pet);

            var token = new CancellationTokenSource().Token;

            var volunteerRepsitoryMock = new Mock<IVolunteerRepository>();
            volunteerRepsitoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(volunteerMockReturn, true, Error.None)));

            var fileProviderMock = new Mock<IFileProvider>();
            fileProviderMock.Setup(p => p.UploadFile(It.IsAny<FileData>(), token))
            .Returns(Task.Run(() => new Result<string>("test", true, Error.None)));

            var loggerMock = new Mock<ILogger<AddPetFilesHandler>>();

            var addPetFilesHandler = new AddPetFilesHandler(
                volunteerRepsitoryMock.Object,
                fileProviderMock.Object,
                loggerMock.Object);

            var filesCount = 10;
            var filesList = new List<FileDto>();
            for (int i = 0; i < filesCount; i++)
            {
                var name = "test";
                var content = "image/jpeg";
                using var stream = new MemoryStream();
                var files = new FileDto(stream, name, content);
                filesList.Add(files);
            }

            var volunteerId = Guid.NewGuid();
            var petId = pet.Id.Id;
            var addPetFileCommand =
                    new AddPetFilesCommand(volunteerId, petId, filesList);

            //Act
            var result = await addPetFilesHandler.Execute(addPetFileCommand, token);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(filesCount, result.Value);
        }
    }
}
