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

        public UpdateMainInfoHandler(IVolunteerRepository volunteerRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            UpdateMainInfoRequest request,
            CancellationToken token)
        {
            var volunteerId = VolunteerId.Create(request.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var fullName = FullName.Create(
                request.MainInfoDto.FullName.FirstName,
                request.MainInfoDto.FullName.SecondName,
                request.MainInfoDto.FullName.Surname).Value;

            var email = Email.Create(request.MainInfoDto.Email).Value;

            var phone = Phone.Create(request.MainInfoDto.Phone).Value;

            var description = VolunteerDescription
                .Create(request.MainInfoDto.Description).Value;

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
