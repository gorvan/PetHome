using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.InitialRequest;
public class InitialRequestHandler : ICommandHandler<Guid, InitialRequestCommand>
{
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly ILogger<InitialRequestHandler> _logger;
    private readonly IValidator<InitialRequestCommand> _validator;

    public InitialRequestHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        ILogger<InitialRequestHandler> logger,
        IValidator<InitialRequestCommand> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(InitialRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var requestId = RequestId.Create(command.RequestId);
        var userId = UserId.Create(command.UserId);

        var fullName = FullName.Create(
            command.FullName.FirstName,
            command.FullName.SecondName,
            command.FullName.Surname).Value;

        var email = Email.Create(command.Email).Value;
        var description = DescriptionValueObject.Create(command.Description).Value;
        var phone = Phone.Create(command.Phone).Value;
        var volunteerInfo = new VolunteerInfo(fullName, email, description, phone);

        var requestResult = VolunteerRequest.Create(
            requestId,
            userId,
            volunteerInfo,
            command.Status,
            command.CreateAt);

        return await _volunteerRequestRepository.Add(requestResult, cancellationToken);
    }
}
