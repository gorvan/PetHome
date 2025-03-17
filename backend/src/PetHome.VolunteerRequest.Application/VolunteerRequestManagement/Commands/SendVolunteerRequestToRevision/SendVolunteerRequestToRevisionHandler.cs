using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.SendVolunteerRequestToRevision;
public class SendVolunteerRequestToRevisionHandler
    : ICommandHandler<SendVolunteerRequestToRevisionCommand>
{
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SendVolunteerRequestToRevisionCommand> _validator;

    public SendVolunteerRequestToRevisionHandler(
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromServices] IUnitOfWork unitOfWork,
        IValidator<SendVolunteerRequestToRevisionCommand> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Execute(SendVolunteerRequestToRevisionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        var requestId = RequestId.Create(command.VolunteerRequestId);

        var volunteerRequestResult = await _volunteerRequestRepository.GetById(requestId, cancellationToken);

        if (volunteerRequestResult.IsFailure)
        {
            transaction.Rollback();
            return volunteerRequestResult.Error;
        }

        var adminId = AdminId.Create(command.AdminId);

        var commentResult = Comment.Create(command.Comment);

        if (commentResult.IsFailure)
        {
            transaction.Rollback();
            return commentResult.Error;
        }

        var disscusionId = DisscusionId.Create(command.DisscusionId);

        volunteerRequestResult.Value.SendToRevision(commentResult.Value, disscusionId);

        await _volunteerRequestRepository.Update(volunteerRequestResult.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
