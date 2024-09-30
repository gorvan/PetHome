using PetHome.Domain.Shared;

namespace PetHome.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string Nickname { get; init; } = string.Empty;
        public Guid SpeciesId { get; init; }
        public Guid BreedId { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Color { get; init; } = string.Empty;
        public string Health { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Street { get; init; } = string.Empty;
        public string House { get; init; } = string.Empty;
        public string Appartment { get; init; } = string.Empty;
        public string Phone { get; } = string.Empty;
        public DateTime BirthDay { get; } = default!;
        public DateTime CreateDate { get; } = default!;
        public RequisiteDto[] Requisites { get; } = [];
        public bool IsNeutered { get; }
        public bool IsVaccinated { get; }
        public HelpStatus HelpStatus { get; }
        public double Weight { get; } = 0;
        public double Height { get; } = 0;
        public int SerialNumber { get; private set; }
        public PhotoDto[] Photos { get; init; } = [];
    }
}
