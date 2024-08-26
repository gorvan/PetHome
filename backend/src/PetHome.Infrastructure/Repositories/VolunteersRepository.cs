using Microsoft.EntityFrameworkCore;
using PetHome.Application.Volunteers;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _context;
        public VolunteersRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<Guid>> Add(Volunteer volunteer, CancellationToken token)
        {
            await _context.Volunteers.AddAsync(volunteer, token);
            await _context.SaveChangesAsync(token);
            return Result<Guid>.Success(volunteer.Id);
        }

        public async Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken token)
        {
            var volunteer = await _context.Volunteers
                .Include(v => v.Requisites)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer is null)
            {
                return Errors.General.NotFound(id);
            }

            return volunteer;
        }

        public async Task<Result<Volunteer>> GetByPhone(Phone phone, CancellationToken token)
        {
            var volunteer = await _context
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
