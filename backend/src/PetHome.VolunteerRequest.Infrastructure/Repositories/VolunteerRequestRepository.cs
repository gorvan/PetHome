using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Application.VolunteerRequestManagement;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Infrastructure.DbContexts;

namespace PetHome.VolunteerRequests.Infrastructure.Repositories;
public class VolunteerRequestRepository : IVolunteerRequestRepository
{
    private readonly WriteDbContext _dbContext;
    public VolunteerRequestRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(
        VolunteerRequest volunteerRequest, 
        CancellationToken token)
    {
        await _dbContext.Volunteers.AddAsync(volunteerRequest, token);
        await _dbContext.SaveChangesAsync(token);
        return Result<Guid>.Success(volunteerRequest.RequestId);
    }

    public Task<Result<Guid>> Delete(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<VolunteerRequest>> GetById(VolunteerId id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<VolunteerRequest>> GetByPhone(Phone phone, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> Restore(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> Update(VolunteerRequest volunteerRequest, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
