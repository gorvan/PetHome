using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;


namespace PetHome.Application.VolunteersManagement.Create
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<CreateVolunteerHandler> _logger;
        private readonly IValidator<CreateVolunteerCommand> _validator;

        public CreateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<CreateVolunteerHandler> logger,
            IValidator<CreateVolunteerCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            CreateVolunteerCommand command,
            CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var phone = Phone.Create(command.phone).Value;

            var existVolunteerResult = await _volunteerRepository
                .GetByPhone(phone, token);

            if (existVolunteerResult.IsSuccess)
                return Errors.General.AlreadyExist();

            var volunteer = CreateVolunteer(command, phone);

            await _volunteerRepository.Add(volunteer, token);

            _logger.LogInformation("Add new volunteer, Id: {volunteerId}", volunteer.Id);

            return (Guid)volunteer.Id;
        }

        private Volunteer CreateVolunteer(CreateVolunteerCommand request, Phone phone)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullName = FullName.Create(
                request.fullName.FirstName,
                request.fullName.SecondName,
                request.fullName.Surname).Value;

            var email = Email.Create(request.email).Value;

            var description = VolunteerDescription
                .Create(request.description).Value;

            var socialColl = (from item in request.socialNetworkDtos
                              let socialNetwork = SocialNetwork
                                    .Create(item.Name, item.Path).Value
                              select socialNetwork).ToList();

            var socialNetworkCollection = new SocialNetworks(socialColl);

            var requisiteColl = (from item in request.requisiteDtos
                                 let requisite = Requisite
                                    .Create(item.Name, item.Description).Value
                                 select requisite).ToList();

            var requisiteCollection = new VolunteersRequisites(requisiteColl);

            return new Volunteer(
                volunteerId,
                fullName,
                email,
                description,
                phone,
                socialNetworkCollection,
                requisiteCollection);
        }
    }
}
