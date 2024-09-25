using Microsoft.EntityFrameworkCore;
using PetHome.Application.VolunteersManagement;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using PetHome.Infrastructure.DbContexts;

namespace PetHome.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteerRepository
    {
        private readonly WriteDbContext _dbContext;
        public VolunteersRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(Volunteer volunteer, CancellationToken token)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, token);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(volunteer.Id);
        }

        public async Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken token)
        {
            _dbContext.Volunteers.Attach(volunteer);
            await _dbContext.SaveChangesAsync();
            return Result<Guid>.Success(volunteer.Id);
        }

        public async Task<Result<Guid>> Delete(Volunteer volunteer, CancellationToken token)
        {
            _dbContext.Volunteers.Remove(volunteer);
            await _dbContext.SaveChangesAsync(token);
            return Result<Guid>.Success(volunteer.Id);
        }

        public Task<Result<Guid>> Restore(Volunteer volunteer, CancellationToken token)
        {
            volunteer.Restore();
            return Update(volunteer, token);
        }

        public async Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken token)
        {
            var volunteer = await _dbContext
                .Volunteers
                .FirstOrDefaultAsync(v => v.Id == id, token);

            if (volunteer is null)
            {
                return Errors.General.NotFound(id);
            }

            return volunteer;
        }

        public async Task<Result<Volunteer>> GetByPhone(Phone phone, CancellationToken token)
        {
            var volunteer = await _dbContext
                .Volunteers
                .FirstOrDefaultAsync(v => v.Phone == phone, token);

            if (volunteer == null)
            {
                return Errors.General.NotFound();
            }

            return volunteer;
        }
    }
}
