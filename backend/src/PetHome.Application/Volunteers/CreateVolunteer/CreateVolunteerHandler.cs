using PetHome.Domain.PetManadgement.AggregateRoot;
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
            var phone = Phone.Create(request.phone).Value;

            var existVolunteerResult = await _volunteerRepository
                .GetByPhone(phone, token);

            if (existVolunteerResult.IsSuccess)
                return Errors.General.AlreadyExist();

            var volunteerId = VolunteerId.NewVolunteerId();

            var fullName = FullName.Create(
                request.fullName.firstName,
                request.fullName.secondName,
                request.fullName.surname).Value;

            var email = Email.Create(request.email).Value;

            var description = VolunteerDescription
                .Create(request.description).Value;

            var socialColl = (from item in request.socialNetworkDtos
                              let socialNetwork = SocialNetwork
                                    .Create(item.name, item.path).Value
                              select socialNetwork).ToList();

            var socialNetworkCollection = new SocialNetworks(socialColl);

            var requisiteColl = (from item in request.requisiteDtos
                                 let requisite = Requisite
                                    .Create(item.name, item.description).Value
                                 select requisite).ToList();

            var requisiteCollection = new VolunteersRequisites(requisiteColl);

            var volunteer = new Volunteer(
                volunteerId,
                fullName,
                email,
                description,
                phone,
                socialNetworkCollection,
                requisiteCollection);

            await _volunteerRepository.Add(volunteer, token);

            return (Guid)volunteer.Id;
        }
    }
}
