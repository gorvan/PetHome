using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public async Task<Result<Guid>> Execute(CreateVolunteerRequest request, CancellationToken token)
        {
            var phoneCheckResult = Phone.Create(request.phone);
            if (phoneCheckResult.IsFailure)
            {
                return phoneCheckResult.Error!;
            }

            var existVolunteerResult = await _volunteerRepository
                .GetByPhone(phoneCheckResult.Value, token);

            if (existVolunteerResult.Error == Error.None)
            {
                return Errors.General.AlreadyExist();
            }

            var volunteerId = VolunteerId.NewVolunteerId();

            var fullNameResult = FullName.Create(
                request.firstName,
                request.secondName,
                request.surname);

            if (fullNameResult.IsFailure)
            {
                return fullNameResult.Error!;
            }

            var emailResult = Email.Create(request.emaile);

            if (emailResult.IsFailure)
            {
                return emailResult.Error!;
            }

            var descriptionResult = VolunteerDescription.Create(request.description);

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

            var socialNetworkCollectionResult = new SocialNetworks(socialColl);
            var requisiteColl = new List<Requisite>();

            foreach (var item in request.requisiteDtos)
            {
                var requisite = Requisite.Create(item.name, item.description);
                if (requisite.IsFailure)
                {
                    return requisite.Error!;
                }

                requisiteColl.Add(requisite.Value);
            }

            var requisiteCollectionResult = new VolunteersRequisites(requisiteColl);

            var volunteer = new Volunteer(
                volunteerId,
                fullNameResult.Value,
                emailResult.Value,
                descriptionResult.Value,
                phoneCheckResult.Value,
                socialNetworkCollectionResult,
                requisiteCollectionResult,
                new List<Pet>(),
                0);

            await _volunteerRepository.Add(volunteer, token);

            return (Guid)volunteer.Id;
        }
    }
}
