using Microsoft.EntityFrameworkCore;
using PetHome.Volunteers.Infrastructure.DbContexts;

namespace PetHome.Volunteers.Infrastructure.Services
{
    internal class DeleteExpiredEntitiesService(WriteDbContext writeDbContext)
    {
        public async Task Execute(CancellationToken cancellationToken)
        {
            var volunteers = await writeDbContext
                .Volunteers.Include(v => v.Pets).ToListAsync(cancellationToken);

            foreach (var volunteer in volunteers)
            {
                if(volunteer.IsExpired)
                {
                    writeDbContext.Volunteers.Remove(volunteer);
                    continue;
                }

                volunteer.DeleteExpiredPets();
            }

            await writeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
