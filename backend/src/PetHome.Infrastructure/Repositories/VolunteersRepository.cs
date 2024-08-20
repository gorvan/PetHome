using Microsoft.EntityFrameworkCore;
using PetHome.Application.Volunteers;
using PetHome.Domain.Models.CommonModels;
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
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer is null)
            {
                return "Volunteer not found";
            }

            return volunteer;
        }

        public async Task<Result<Volunteer>> GetByPhone(Phone phone, CancellationToken token)
        {
            try
            {
                var res = await _context
                    .Volunteers
                    .FirstOrDefaultAsync(v => v.Phone == phone, token);
                if (res == null)
                {
                    return "Volunteers is null";
                }
                return res;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
