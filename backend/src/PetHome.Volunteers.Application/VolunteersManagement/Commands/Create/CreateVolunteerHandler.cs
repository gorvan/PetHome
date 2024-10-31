using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain;
using PetHome.Volunteers.Domain.ValueObjects;


namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.Create
{
    public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
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

            var phone = Phone.Create(command.Phone).Value;

            var existVolunteerResult = await _volunteerRepository
                .GetByPhone(phone, token);

            if (existVolunteerResult.IsSuccess)
            {
                return Errors.General.AlreadyExist();
            }

            var volunteer = CreateVolunteer(command, phone);

            await _volunteerRepository.Add(volunteer, token);

            _logger.LogInformation("Add new volunteer, Id: {volunteerId}", volunteer.Id);

            return (Guid)volunteer.Id;
        }

        private Volunteer CreateVolunteer(CreateVolunteerCommand request, Phone phone)
        {
            var volunteerId = VolunteerId.NewVolunteerId();

            var fullName = FullName.Create(
                request.FullName.FirstName,
                request.FullName.SecondName,
                request.FullName.Surname).Value;

            var email = Email.Create(request.Email).Value;

            var description = Description
                .Create(request.Description).Value;

            var socialList = (from item in request.SocialNetworkDtos
                              let socialNetwork = SocialNetwork
                                    .Create(item.Name, item.Path).Value
                              select socialNetwork).ToList();

            var requisiteList = (from item in request.RequisiteDtos
                                 let requisite = Requisite
                                    .Create(item.Name, item.Description).Value
                                 select requisite).ToList();

            return new Volunteer(
                volunteerId,
                fullName,
                email,
                description,
                phone,
                socialList,
                requisiteList,
                request.Experience);
        }
    }
}
