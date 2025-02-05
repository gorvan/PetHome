using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain;
using PetHome.Volunteers.Domain.Entities;
using PetHome.Volunteers.Domain.ValueObjects;

namespace PetHome.UnitTests
{
    public class VolunteerTests
    {
        public static Volunteer CreateVolunteer()
        {
            var id = VolunteerId.Empty();
            var name = FullName.Create("Test", "Test", "Test").Value;
            var email = Email.Create("test@ddd.ru").Value;
            var description = DescriptionValueObject.Create("Test").Value;
            var phone = Phone.Create("+70952365484").Value;
            var socialnetworks = new List<SocialNetwork>();
            var requisites = new List<Requisite>();
            int experience = 1;

            return new Volunteer(
                id,
                name,
                email,
                description,
                phone,
                socialnetworks,
                requisites,
                experience);
        }

        public static Pet CreatePet()
        {
            var id = PetId.Empty();
            var nickname = PetNickname.Create("test").Value;
            var speciesBreed =
                new SpeciesBreedValue(SpeciesId.Empty(), BreedId.Empty());
            var description = DescriptionValueObject.Create("test").Value;
            var color = PetColor.Create("test").Value;
            var health = HealthInfo.Create("test").Value;
            var address = Address.Create("test", "test", "test", "test").Value;
            var phone = Phone.Create("+70952365484").Value;
            var requisites = new List<Requisite>();
            var birthDay = DateValue.Create(DateTime.Now).Value;
            var createDate = DateValue.Create(DateTime.Now).Value;
            var isNeutered = true;
            var isVaccinated = true;
            var helpStatus = HelpStatus.NeedHelp;
            var weight = 5;
            var height = 4;

            return new Pet(
                id,
                nickname,
                speciesBreed,
                description,
                color,
                health,
                address,
                phone,
                requisites,
                birthDay,
                createDate,
                isNeutered,
                isVaccinated,
                helpStatus,
                weight,
                height);
        }

        [Fact]
        public void AddPet_ItShould_Add_New_Pet_To_Pet_List()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet = CreatePet();
            var expected = 1;

            //Act
            volunteer.AddPet(pet);

            //Assert
            Assert.Equal(expected, volunteer.Pets.Count);
            Assert.Equal(expected, pet.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Not_Change_Any_If_New_SerialNumber_Equals_Old_SerialNumber()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet = CreatePet();
            volunteer.AddPet(pet);
            var number = 1;
            var newSerialNumber = SerialNumber.Create(number).Value;

            //Act
            volunteer.MovePet(pet, newSerialNumber);

            //Assert
            Assert.Equal(number, pet.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Not_Set_SerialNumber_Less_Than_1_When_Pet_1()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet = CreatePet();
            volunteer.AddPet(pet);
            var number = 1;
            var newSerialNumber = SerialNumber.Create(number).Value;

            //Act
            volunteer.MovePet(pet, newSerialNumber);

            //Assert
            Assert.Equal(number, pet.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Not_Set_SerialNumber_Less_Than_1_When_Pets_2()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            var number = 1;
            var newSerialNumber = SerialNumber.Create(number).Value;

            //Act
            volunteer.MovePet(pet2, newSerialNumber);

            //Assert
            Assert.Equal(number, pet2.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Not_Set_SerialNumber_More_Than_Volunteer_Pets_Count()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet = CreatePet();
            volunteer.AddPet(pet);
            var number = 2;
            var test = 1;
            var newSerialNumber = SerialNumber.Create(number).Value;

            //Act
            volunteer.MovePet(pet, newSerialNumber);

            //Assert
            Assert.Equal(test, pet.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Not_Set_SerialNumber_More_Than_Volunteer_Pets_Count_Pets_2()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            var number = 2;
            var test = 2;
            var newSerialNumber = SerialNumber.Create(number).Value;

            //Act
            volunteer.MovePet(pet1, newSerialNumber);

            //Assert
            Assert.Equal(test, pet1.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_2_Change_To_4()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 4;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet2, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet2.SerialNumber.Value);
            Assert.Equal(1, pet1.SerialNumber.Value);
            Assert.Equal(2, pet3.SerialNumber.Value);
            Assert.Equal(3, pet4.SerialNumber.Value);
            Assert.Equal(5, pet5.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_2_Change_To_5()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 5;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet2, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet2.SerialNumber.Value);
            Assert.Equal(1, pet1.SerialNumber.Value);
            Assert.Equal(2, pet3.SerialNumber.Value);
            Assert.Equal(3, pet4.SerialNumber.Value);
            Assert.Equal(4, pet5.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_1_Change_To_5()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 5;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet1, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet1.SerialNumber.Value);
            Assert.Equal(1, pet2.SerialNumber.Value);
            Assert.Equal(2, pet3.SerialNumber.Value);
            Assert.Equal(3, pet4.SerialNumber.Value);
            Assert.Equal(4, pet5.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_4_Change_To_2()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 2;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet4, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet4.SerialNumber.Value);
            Assert.Equal(1, pet1.SerialNumber.Value);
            Assert.Equal(3, pet2.SerialNumber.Value);
            Assert.Equal(4, pet3.SerialNumber.Value);
            Assert.Equal(5, pet5.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_4_Change_To_1()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 1;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet4, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet4.SerialNumber.Value);
            Assert.Equal(2, pet1.SerialNumber.Value);
            Assert.Equal(3, pet2.SerialNumber.Value);
            Assert.Equal(4, pet3.SerialNumber.Value);
            Assert.Equal(5, pet5.SerialNumber.Value);
        }

        [Fact]
        public void MovePet_ItShould_Set_Right_SerialNumber_If_Initial_Number_5_Change_To_1()
        {
            //Arrange
            var volunteer = CreateVolunteer();
            var pet1 = CreatePet();
            var pet2 = CreatePet();
            var pet3 = CreatePet();
            var pet4 = CreatePet();
            var pet5 = CreatePet();

            volunteer.AddPet(pet1);
            volunteer.AddPet(pet2);
            volunteer.AddPet(pet3);
            volunteer.AddPet(pet4);
            volunteer.AddPet(pet5);

            var newNumber = 1;
            var newSerialNumber = SerialNumber.Create(newNumber).Value;

            //Act
            volunteer.MovePet(pet5, newSerialNumber);

            //Assert
            Assert.Equal(newNumber, pet5.SerialNumber.Value);
            Assert.Equal(2, pet1.SerialNumber.Value);
            Assert.Equal(3, pet2.SerialNumber.Value);
            Assert.Equal(4, pet3.SerialNumber.Value);
            Assert.Equal(5, pet4.SerialNumber.Value);
        }
    }
}