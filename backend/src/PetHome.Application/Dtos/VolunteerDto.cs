namespace PetHome.Application.Dtos
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
        public string SocialNetworks { get; init; } = string.Empty;       
        public string Requisites { get; } = string.Empty;        
        public PetDto[] Pets { get; init; } = [];
    }
}
