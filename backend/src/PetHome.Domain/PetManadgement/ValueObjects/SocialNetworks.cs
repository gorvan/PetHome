namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record SocialNetworks
    {
        public SocialNetworks() { }
        public SocialNetworks(IEnumerable<SocialNetwork> socialNets)
        {
            Networks = socialNets.ToList();
        }

        public IReadOnlyList<SocialNetwork> Networks { get; }
    }
}
