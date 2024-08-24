namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record SocialNetworks
    {
        public SocialNetworks() { }
        public SocialNetworks(IEnumerable<SocialNetwork> socialNets)
        {
            SocialNets = socialNets.ToList();
        }

        public IReadOnlyList<SocialNetwork> SocialNets { get; } = [];
    }
}
