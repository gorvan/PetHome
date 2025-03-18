using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHome.Disscusions.Contracts;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.AproveVolunteerRequest;
public class AproveVolunteerRequestHandler : ICommandHandler<AproveVolunteerRequestCommand>
{
    private readonly IDisscusionContract _disscusionContract;
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AproveVolunteerRequestCommand> _validator;

    public AproveVolunteerRequestHandler(
        IDisscusionContract disscusionContract,
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromServices] IUnitOfWork unitOfWork,
        IValidator<AproveVolunteerRequestCommand> validator)
    {
        _disscusionContract = disscusionContract;
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Execute(AproveVolunteerRequestCommand command, CancellationToken cancellationToken)
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

        var closeDisscusionResult = await _disscusionContract.CloseDisscusion(
            volunteerRequestResult.Value.DisscusionId,
            cancellationToken);

        if (closeDisscusionResult.IsFailure)
        {
            transaction.Rollback();
            return closeDisscusionResult.Error;
        }

        volunteerRequestResult.Value.ApproveRequest();

        await _volunteerRequestRepository.Update(volunteerRequestResult.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        transaction.Commit();

        return Result.Success();
    }
}
