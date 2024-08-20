using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Models.Volunteers;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        //validation
        //domain model creating
        //save to database

        public async Task<Result<Guid>> Execute(CreateVolunteerRequest request, CancellationToken token)
        {
            var phoneResult = Phone.Create(request.phone);
            if (phoneResult.IsFailure)
            {
                return phoneResult.Error!;
            }

            var existVolunteerResult = await _volunteerRepository.GetByPhone(phoneResult.Value, token);

            if (existVolunteerResult.IsSuccess)
            {
                return "Volunteer must be unique by phone";
            }

            var fullNameResult = FullName.Create(request.firstName, request.secondName, request.surname);

            if (fullNameResult.IsFailure)
            {
                return fullNameResult.Error!;
            }

            var emailResult = Email.Create(request.emaile);

            if (emailResult.IsFailure)
            {
                return emailResult.Error!;
            }

            var descriptionResult = Description.Create(request.description);

            if (descriptionResult.IsFailure)
            {
                return descriptionResult.Error!;
            }

            var socialColl = new List<SocialNetwork>();

            foreach (var item in request.socialNetworkDtos)
            {               
                var socialNetworkResult = SocialNetwork
                .Create(item.name, item.path);
                if (socialNetworkResult.IsFailure)
                {
                    return socialNetworkResult.Error!;
                }

                socialColl.Add(socialNetworkResult.Value);
            }

            var socialNetworkCollectionResult = SocialNetworkCollection.Create(socialColl);
            if (socialNetworkCollectionResult.IsFailure)
            {
                return socialNetworkCollectionResult.Error!;
            }

            var requisiteColl = new List<Requisite>();

            foreach (var item in request.requisiteDtos)
            {  
                var requisite = Requisite.Create(item.name, item.description);
                if(requisite.IsFailure)
                {
                    return requisite.Error!;
                }

                requisiteColl.Add(requisite.Value);
            }

            var requisiteCollectionResult = RecuisiteCollection.Create(requisiteColl);
            if(requisiteCollectionResult.IsFailure)
            {
                return requisiteCollectionResult.Error!;
            }            

            var volunteerResult = Volunteer.Create(fullNameResult.Value, emailResult.Value,
                descriptionResult.Value, phoneResult.Value, socialNetworkCollectionResult.Value,
                 requisiteCollectionResult.Value);

            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error!;
            }

            await _volunteerRepository.Add(volunteerResult.Value, token);
            return (Guid)volunteerResult.Value.Id;
        }
    }
}
