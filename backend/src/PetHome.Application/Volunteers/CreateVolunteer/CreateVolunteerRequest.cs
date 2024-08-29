namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        FullNameDto fullName,
        string email,
        string description,
        string phone,
        List<SocialNetworkDto> socialNetworkDtos,
        List<RequisiteDto> requisiteDtos);

    public record FullNameDto(
        string firstName,
        string secondName,
        string surname);

    public record SocialNetworkDto(
        string name,
        string path);

    public record RequisiteDto(
        string name,
        string description);

}
