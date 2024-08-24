using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record SocialNetwork
    {
        private SocialNetwork(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; } = default!;
        public string Link { get; } = default!;

        public static Result<SocialNetwork> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.General.ValueIsRequeired("SocialNetwork.Name");
            }

            if (string.IsNullOrWhiteSpace(name) 
                || link.Length > Constants.MAX_PATH_LENGTH)
            {
                return Errors.General.ValueIsInvalid("SocialNetwork.Link");
            }

            return new SocialNetwork(name, link);
        }
    }
}
