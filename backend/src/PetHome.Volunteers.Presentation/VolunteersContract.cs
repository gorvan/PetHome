using PetHome.Shared.Core.Dtos;
using PetHome.Volunteers.Application;
using PetHome.Volunteers.Contracts;

namespace PetHome.Volunteers.Presentation
{
    public class VolunteersContract : IVolunteersContract
    {
        private readonly IReadDbContextVolunteers _readDbContext;
        public VolunteersContract(IReadDbContextVolunteers readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public IQueryable<PetDto> GetPetDtos()
        {
            return _readDbContext.Pets;
        }
    }
}
