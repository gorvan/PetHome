using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Models.Pets;
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
            if(phoneResult.IsFailure)
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

            foreach (var item in request.SocialNetworkDtos)
            {
                var networkNameResult = NotNullableString.Create(item.name);
                if (networkNameResult.IsFailure)
                {
                    return networkNameResult.Error!;
                }

                var networkPathResult = NotNullableText.Create(item.path);
                if (networkPathResult.IsFailure)
                {
                    return networkPathResult.Error!;
                }

                var socialNetworkResult = SocialNetwork
                .Create(networkNameResult.Value, networkPathResult.Value);
                if (socialNetworkResult.IsFailure)
                {
                    return socialNetworkResult.Error!;
                }

                socialColl.Add(socialNetworkResult.Value);
            }            

            var socialNetworkCollectionResult = SocialNetworkCollection.Create(socialColl); 
            if(socialNetworkCollectionResult.IsFailure)
            {
                return socialNetworkCollectionResult.Error!;
            }

            var requisiteIdResult = RequisiteId.NewRequisiteId();           

            var requisiteNameResult = NotNullableString.Create(request.requisiteName);
            if (requisiteNameResult.IsFailure)
            {
                return requisiteNameResult.Error!;
            }

            var requisiteDescriptionResult = Description
                .Create(request.requisiteDescription);
            if (requisiteDescriptionResult.IsFailure)
            {
                return requisiteDescriptionResult.Error!;
            }

            var requisiteResult = Requisite.Create(requisiteIdResult, 
                    requisiteNameResult.Value, requisiteDescriptionResult.Value);
            if(requisiteResult.IsFailure)
            {
                return requisiteResult.Error!;
            }                

            var volunteerResult = Volunteer.Create(fullNameResult.Value, emailResult.Value,
                descriptionResult.Value, phoneResult.Value, socialNetworkCollectionResult.Value,
                 requisiteResult.Value);

            if(volunteerResult.IsFailure)
            {
                return volunteerResult.Error!;
            }

            await _volunteerRepository.Add(volunteerResult.Value, token);
            return (Guid)volunteerResult.Value.Id;
        }
    }
}
