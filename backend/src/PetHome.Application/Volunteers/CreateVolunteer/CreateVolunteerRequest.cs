namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        string firstName, 
        string secondName,
        string surname, 
        string emaile, 
        string description, 
        string phone,
        List<SosialNetworkDto> SocialNetworkDtos,
        string requisiteName, 
        string requisiteDescription);

    public record SosialNetworkDto(
        string name, 
        string path);
}
