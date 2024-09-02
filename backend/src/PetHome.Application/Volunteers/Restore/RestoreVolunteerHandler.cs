using Microsoft.Extensions.Logging;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.Restore
{
    public class RestoreVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<RestoreVolunteerHandler> _logger;

        public RestoreVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<RestoreVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            RestoreVolunteerRequest request,
            CancellationToken token)
        {
            var volunteerId = VolunteerId.Create(request.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var result = await _volunteerRepository.Restore(volunteerResult.Value, token);

            _logger.LogInformation("Restore volunteer with id {volunteerId}", volunteerResult);

            return volunteerResult.Value.Id.Id;
        }
    }
}
