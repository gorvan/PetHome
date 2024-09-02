namespace PetHome.Application.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoRequest(
        Guid VolunteerId,
        UpdateMainInfoDto MainInfoDto);
}
