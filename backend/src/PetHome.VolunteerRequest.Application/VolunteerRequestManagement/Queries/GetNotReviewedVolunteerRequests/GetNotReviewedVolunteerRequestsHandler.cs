using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetNotReviewedVolunteerRequests;

public class GetNotReviewedVolunteerRequestsHandler
    : IQueryHandler<Result<PagedList<VolunteerRequestDto>>, GetNotReviewedVolunteerRequestsQuery>
{
    private readonly IreadDbContextVolunteerRequest _volunteerRequestRepository;
    private readonly ILogger<GetNotReviewedVolunteerRequestsHandler> _logger;
    private readonly IValidator<GetNotReviewedVolunteerRequestsQuery> _validator;

    public GetNotReviewedVolunteerRequestsHandler(
        IreadDbContextVolunteerRequest volunteerRequestRepository,
        ILogger<GetNotReviewedVolunteerRequestsHandler> logger,
        IValidator<GetNotReviewedVolunteerRequestsQuery> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>>> Execute(
        GetNotReviewedVolunteerRequestsQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        return await _volunteerRequestRepository
            .VolunteerRequests
            .Where(v => v.Status == RequestStatus.None)
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
