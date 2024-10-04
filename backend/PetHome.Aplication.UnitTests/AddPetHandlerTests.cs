using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.SpeciesManagement;
using PetHome.Application.VolunteersManagement;
using PetHome.Application.VolunteersManagement.Commands.PetManagement.AddPet;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.Entities;
using PetHome.Domain.SpeciesManagement.ValueObjects;

namespace PetHome.UnitTests
{
    public class AddPetHandlerTests
    {
        public static AddPetCommand CreatePetCommand()
        {
            var nick = "test";
            var speciesId = Guid.Empty;
            var breedId = Guid.Empty;
            var description = "test";
            var color = "test";
            var health = "test";
            var address = new AddressDto("test", "test", "test", "test");
            var phone = "+70958526341";
            var requisites = new RequisiteDto("test", "test");
            var birthday = DateTime.Now;
            var isNeutered = true;
            var isVacinated = true;
            var helpStatus = HelpStatus.NeedHelp;
            var weight = 1.0;
            var height = 1.0;

            return new AddPetCommand(
                Guid.NewGuid(), nick, speciesId, breedId, description, color, health, address, phone,
                requisites, birthday, isNeutered, isVacinated, helpStatus, weight, height);
        }

        public static Species GetSpecies()
        {
            var speciesId = SpeciesId.Empty();
            var speciesName = "species";
            var breedId = BreedId.Empty();
            var breedName = "breed";
            var breed = Breed.Create(breedId, breedName);
            return Species.Create(speciesId, speciesName, [breed.Value]).Value;
        }

        [Fact]
        public async Task AddPet_To_Volunteer_Success()
        {
            //Arrange
            var token = new CancellationTokenSource().Token;
            var volunteerRepositoryMock = new Mock<IVolunteerRepository>();
            volunteerRepositoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(VolunteerTests.CreateVolunteer(), true, Error.None)));

            var speciesRepositoryMock = new Mock<ISpeciesRepository>();
            speciesRepositoryMock
                .Setup(v => v.GetById(It.IsAny<SpeciesId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Species>(GetSpecies(), true, Error.None)));

            var readDbContextMock = new Mock<IReadDbContext>();
            readDbContextMock.Setup(r => r.Species).Returns(
                new List<SpeciesDto>
                {
                    new SpeciesDto
                    {
                        Id = Guid.Empty,
                        Name = "Test",
                        Breeds = [
                            new BreedDto
                            {
                                Id = Guid.Empty,
                                Name = "Test",
                                SpeciesId = Guid.Empty,
                            }]
                    }
                }.AsQueryable());

            var loggerMock = new Mock<ILogger<AddPetHandler>>();

            var validatorMock = new Mock<IValidator<AddPetCommand>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AddPetCommand>(), token))
                .Returns(Task.Run(() => new ValidationResult { Errors = [] }));

            var addPetHandler = new AddPetHandler(
                volunteerRepositoryMock.Object,
                speciesRepositoryMock.Object,
                readDbContextMock.Object,
                loggerMock.Object,
                validatorMock.Object);

            var command = CreatePetCommand();

            //Act
            var result = await addPetHandler.Execute(command, token);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddPet_Should_Return_Error_If_Volunteer_Not_Found_By_Id()
        {
            //Arrange
            var testResult = Errors.General.NotFound();
            var token = new CancellationTokenSource().Token;
            var volunteerRepsitoryMock = new Mock<IVolunteerRepository>();
            volunteerRepsitoryMock
                .Setup(v => v.GetById(It.IsAny<VolunteerId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Volunteer>(VolunteerTests.CreateVolunteer(), false,
                testResult)));

            var speciesRepositoryMock = new Mock<ISpeciesRepository>();
            speciesRepositoryMock
                .Setup(v => v.GetById(It.IsAny<SpeciesId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() =>
                new Result<Species>(GetSpecies(), true, Error.None)));

            var readDbContextMock = new Mock<IReadDbContext>();

            var loggerMock = new Mock<ILogger<AddPetHandler>>();

            var validatorMock = new Mock<IValidator<AddPetCommand>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AddPetCommand>(), token))
                .Returns(Task.Run(() => new ValidationResult { Errors = [] }));

            var addPetHandler = new AddPetHandler(
                volunteerRepsitoryMock.Object,
                 speciesRepositoryMock.Object,
                readDbContextMock.Object,
                loggerMock.Object,
                validatorMock.Object);

            var command = CreatePetCommand();

            //Act
            var result = await addPetHandler.Execute(command, token);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(testResult, result.Error);
        }
    }
}
