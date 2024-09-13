using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;       
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteerRepository volunteerRepository,            
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;            
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            UpdateMainInfoCommand command,
            CancellationToken token)
        { 
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var fullName = FullName.Create(
                command.FullName.FirstName,
                command.FullName.SecondName,
                command.FullName.Surname).Value;

            var email = Email.Create(command.Email).Value;

            var phone = Phone.Create(command.Phone).Value;

            var description = VolunteerDescription
                .Create(command.Description).Value;

            volunteerResult.Value.UpdateMainInfo(
                fullName,
                email,
                phone,
                description);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation(
                "Updated volunteer {fullName}, {email}, {phone}, {description} with id {volunteerId}",
                fullName,
                email,
                phone,
                description,
                result);

            return result;
        }
    }
}
