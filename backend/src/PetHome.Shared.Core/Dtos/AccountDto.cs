namespace PetHome.Shared.Core.Dtos;
public class AccountDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string SecondName { get; set; } = default!;
    public string Surname { get; set; } = default!;
}

