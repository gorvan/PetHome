using PetHome.Application.Dtos;

namespace PetHome.Application.Database
{
    public interface IReadDbContext
    {
        IQueryable<VolunteerDto> Volunteers { get; }
        IQueryable<PetDto> Pets { get; }
    }
}
