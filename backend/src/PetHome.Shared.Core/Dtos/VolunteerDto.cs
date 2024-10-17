namespace PetHome.Shared.Core.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string SecondName { get; init; } = string.Empty;
        public string Surname { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public int Experience { get; init; } = 0;
        public SocialNetworkDto[] SocialNetworks { get; set; } = [];
        public RequisiteDto[] Requisites { get; set; } = [];
        public PetDto[] Pets { get; set; } = [];
    }
}
