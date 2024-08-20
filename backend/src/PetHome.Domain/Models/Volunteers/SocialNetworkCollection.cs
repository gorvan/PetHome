using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Volunteers
{
    public record SocialNetworkCollection
    {
        private SocialNetworkCollection() { }
        private SocialNetworkCollection(List<SocialNetwork> networks)
        {
            _socialNetworks = networks;
        }

        private List<SocialNetwork> _socialNetworks { get; } = [];
        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;


        public static Result<SocialNetworkCollection> Create(List<SocialNetwork> networks)
        {
            if (networks is null)
            {
                return $"{nameof(SocialNetworkCollection)} " + $"{nameof(networks)}" + " can not be null";
            }            

            var fullName = new SocialNetworkCollection(networks);

            return fullName;
        }

        public Result Add(SocialNetwork network)
        {
            _socialNetworks.Add(network);
            return Result.Success();
        }
    }
}
