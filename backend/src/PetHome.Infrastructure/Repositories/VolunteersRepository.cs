using Microsoft.EntityFrameworkCore;
using PetHome.Application.Volunteers;
using PetHome.Domain.Models.Volunteers;
using PetHome.Domain.Shared;

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
                .Include(v => v.Detailes)
                .Include(v=>v.Pets)
                .ThenInclude(p => p.Detailes)              
                .Include(v => v.Pets)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(v=>v.Id == id);

            if (volunteer is null)
            {
                return "Volunteer not found";
            }

            return volunteer;
        }

        public async Task<Result<Volunteer>> GetByName(FullName fullName, CancellationToken token)
        {
            if(_context.Volunteers.Count() == 0)
            {
                return null;
            }

            var volunteer = await _context.Volunteers                
                .FirstOrDefaultAsync(v => v.Name == fullName);

            if (volunteer is null)
            {
                return "Volunteer not found";
            }

            return volunteer;
        }
    }
}
