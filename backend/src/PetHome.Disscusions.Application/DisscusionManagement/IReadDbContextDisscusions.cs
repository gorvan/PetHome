using PetHome.Shared.Core.Dtos;

namespace PetHome.Disscusions.Application.DisscusionManagement;
public interface IReadDbContextDisscusions
{
    IQueryable<DisscusionDto> Disscusions { get; }
}
