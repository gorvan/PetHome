using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.UpdateVolunteerRequest;
public class UpdateVolunteerRequestHandler : ICommandHandler<UpdateVolunteerRequestCommand>
{
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly ILogger<UpdateVolunteerRequestHandler> _logger;
    private readonly IValidator<UpdateVolunteerRequestCommand> _validator;

    public UpdateVolunteerRequestHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        ILogger<UpdateVolunteerRequestHandler> logger,
        IValidator<UpdateVolunteerRequestCommand> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result> Execute(
        UpdateVolunteerRequestCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var requestId = RequestId.Create(command.VolunteerRequestId);

        var volunteerRequestResult =
            await _volunteerRequestRepository.GetById(requestId, cancellationToken);

        if (volunteerRequestResult.IsFailure)
        {
            return volunteerRequestResult.Error;
        }

        if (volunteerRequestResult.Value.Status != RequestStatus.Reversion_required)
        {
            return Errors.General.NotFound();
        }

        var userId = UserId.Create(command.UserId);

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.SecondName,
            command.FullName.Surname).Value;

        var email = Email.Create(command.Email).Value;
        var description = DescriptionValueObject.Create(command.Description).Value;
        var phone = Phone.Create(command.Phone).Value;
        var volunteerInfo = VolunteerInfo.Create(fullName, email, description, phone);
        var createAt = DateValue.Create(command.CreateAt).Value;

        var requestResult = VolunteerRequest.Create(
            requestId,
            userId,
            volunteerInfo,
            command.Status,
            createAt);

        return await _volunteerRequestRepository.Update(
            requestResult,
            cancellationToken);
    }
}
