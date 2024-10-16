using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Contracts
{
    public interface IVolunteersContract
    {
        IQueryable<PetDto> GetPetDtos();
    }
}
