using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record PetNickname
    {
        private PetNickname(string nickname)
        {
            Nickname = nickname;
        }

        public string Nickname { get; } = default!;

        public static Result<PetNickname> Create(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                return Errors.General.ValueIsRequeired("Nickname");
            }

            return new PetNickname(nickname);
        }
    }
}
