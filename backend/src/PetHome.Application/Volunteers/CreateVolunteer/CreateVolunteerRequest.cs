namespace PetHome.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(string firstName, string secondName,
        string surname, string emaile, string description, string phone,
        string socialNetworkName, string socialNetworkDescription,
        string requisiteName, string requisiteDescription);
}
