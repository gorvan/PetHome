using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.Models;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVounteerRequestsByVolunteer;
public class GetVounteerRequestsByVolunteerHandler
    : IQueryHandler<Result<PagedList<VolunteerRequestDto>>, GetVounteerRequestsByVolunteerQuery>
{
    private readonly IreadDbContextVolunteerRequest _volunteerRequestRepository;
    private readonly ILogger<GetVounteerRequestsByVolunteerHandler> _logger;
    private readonly IValidator<GetVounteerRequestsByVolunteerQuery> _validator;

    public GetVounteerRequestsByVolunteerHandler(
        IreadDbContextVolunteerRequest volunteerRequestRepository,
        ILogger<GetVounteerRequestsByVolunteerHandler> logger,
        IValidator<GetVounteerRequestsByVolunteerQuery> validator)
    {
        _volunteerRequestRepository = volunteerRequestRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<PagedList<VolunteerRequestDto>>> Execute(
        GetVounteerRequestsByVolunteerQuery query,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        return await _volunteerRequestRepository
            .VolunteerRequests
            .Where(v => v.UserId == query.VolunteerId)
            .WhereIf(query.Status != RequestStatus.None,
                    v => v.Status == query.Status)
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
