namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        string firstName, 
        string secondName,
        string surname, 
        string emaile, 
        string description, 
        string phone,
        List<SocialNetworkDto> socialNetworkDtos,
        List<RequisiteDto> requisiteDtos);

    public record SocialNetworkDto(
        string name, 
        string path);

    public record RequisiteDto(
        string name,
        string description);
}
