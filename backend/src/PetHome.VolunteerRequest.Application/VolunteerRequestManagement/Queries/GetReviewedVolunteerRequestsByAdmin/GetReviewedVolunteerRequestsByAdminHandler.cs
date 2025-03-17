using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetReviewedVolunteerRequestsByAdmin;
public class GetReviewedVolunteerRequestsByAdminHandler
    : IQueryHandler<Result<PagedList<VolunteerRequestDto>>, GetReviewedVolunteerRequestsByAdminQuery>
{
    private readonly IreadDbContextVolunteerRequest _volunteerRequestRepository;
    private readonly ILogger<GetReviewedVolunteerRequestsByAdminHandler> _logger;
    private readonly IValidator<GetReviewedVolunteerRequestsByAdminQuery> _validator;

    public GetReviewedVolunteerRequestsByAdminHandler(
        IreadDbContextVolunteerRequest volunteerRequestRepository,
        ILogger<GetReviewedVolunteerRequestsByAdminHandler> logger,
        IValidator<GetReviewedVolunteerRequestsByAdminQuery> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>>> Execute(
        GetReviewedVolunteerRequestsByAdminQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        return await _volunteerRequestRepository
            .VolunteerRequests
            .Where(v => v.AdminId == query.AdminId)
            .WhereIf(query.Status != RequestStatus.None,
                    v => v.Status == query.Status)
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
