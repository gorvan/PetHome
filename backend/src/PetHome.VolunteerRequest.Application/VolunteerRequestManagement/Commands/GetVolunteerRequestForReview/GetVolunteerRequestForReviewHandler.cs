using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHome.Disscusions.Contracts;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.GetVolunteerRequestForReview;
public class GetVolunteerRequestForReviewHandler
    : ICommandHandler<Guid, GetVolunteerRequestForReviewCommand>
{
    private readonly IDisscusionContract _disscusionContract;
    private readonly IVolunteerRequestRepository _volunteerRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<GetVolunteerRequestForReviewCommand> _validator;

    public GetVolunteerRequestForReviewHandler(
        IDisscusionContract disscusionContract,
        IVolunteerRequestRepository volunteerRequestRepository,
        [FromServices] IUnitOfWork unitOfWork,
        IValidator<GetVolunteerRequestForReviewCommand> validator)
    {
        _disscusionContract = disscusionContract;
        _volunteerRequestRepository = volunteerRequestRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid>> Execute(GetVolunteerRequestForReviewCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        var disscusionResult = await _disscusionContract.CreateDisscusion(command.VolunteerRequestId,
            new List<Guid>
            {
                command.AdminId,
                command.VolunteerId
            },
            cancellationToken);

        if (disscusionResult.IsFailure)
        {
            transaction.Rollback();
            return disscusionResult.Error;
        }

        var requestId = RequestId.Create(command.VolunteerRequestId);

        var volunteerRequestResult = await _volunteerRequestRepository.GetById(requestId, cancellationToken);

        if (volunteerRequestResult.IsFailure)
        {
            transaction.Rollback();
            return volunteerRequestResult.Error;
        }

        var adminId = AdminId.Create(command.AdminId);

        volunteerRequestResult.Value.GetOnReview(adminId);

        await _volunteerRequestRepository.Update(volunteerRequestResult.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        transaction.Commit();

        return disscusionResult;
    }
}
