using Microsoft.EntityFrameworkCore;
using PetHome.Shared.Core.Shared;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.ValueObjects;
using PetHome.VolunteerRequests.Infrastructure.DbContexts;

namespace PetHome.VolunteerRequests.Infrastructure.Repositories;
public class VolunteerRequestRepository : IVolunteerRequestRepository
{
    private readonly WriteDbContext _dbContext;
    public VolunteerRequestRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        await _dbContext.VolunteerRequests.AddAsync(volunteerRequest, token);
        await _dbContext.SaveChangesAsync(token);
        return Result<Guid>.Success(volunteerRequest.RequestId);
    }

    public async Task<Result<Guid>> Delete(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        _dbContext.VolunteerRequests.Remove(volunteerRequest);
        await _dbContext.SaveChangesAsync(token);
        return volunteerRequest.RequestId.Id;
    }

    public async Task<Result<VolunteerRequest>> GetById(RequestId requestId, CancellationToken token)
    {
        var volunteerRequest = await _dbContext.VolunteerRequests
            .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

        if (volunteerRequest == null)
        {
            return Errors.General.NotFound();
        }

        return volunteerRequest;
    }

    public Result<IEnumerable<VolunteerRequest>> GetNotReviewedVolunteerRequests(
        CancellationToken token)
    {
        return _dbContext.VolunteerRequests.Where(v => v.Status == RequestStatus.None).ToList();
    }

    public async Task<Result<Guid>> Update(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        _dbContext.Attach(volunteerRequest);
        await _dbContext.SaveChangesAsync(token);
        return volunteerRequest.RequestId.Id;
    }
}
