using Microsoft.Extensions.Logging;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Application.Volunteers.UpdateRequisites
{
    public class UpdateRequisitesHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateRequisitesHandler> _logger;

        public UpdateRequisitesHandler(IVolunteerRepository volunteerRepository,
            ILogger<UpdateRequisitesHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            UpdateRequisitesCommand request,
            CancellationToken token)
        {
            var volunteerId = VolunteerId.Create(request.VolunteerId);
            var volunteerResult =
                await _volunteerRepository.GetById(volunteerId, token);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var requisiteColl = (from item in request.Requisites
                                 let requisite = Requisite
                                    .Create(item.Name, item.Description).Value
                                 select requisite).ToList();

            var requisiteCollection = new VolunteersRequisites(requisiteColl);

            volunteerResult.Value.UpdateRequisites(
               requisiteCollection);

            var result =
                await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation(
                "Updated requisites with id {volunteerId}",
                result);

            return result;
        }
    }
}
