using PetHome.Domain.Models.CommonModels;
using PetHome.Domain.Shared;

namespace PetHome.Domain.Models.Volunteers
{
    public record SocialNetwork
    {
        private SocialNetwork(NotNullableString name, NotNullableText link) 
        {
            Name = name;
            Link = link;
        }

        public NotNullableString Name { get; } = default!;
        public NotNullableText Link { get; } = default!;

        public static Result<SocialNetwork> Create(NotNullableString name, NotNullableText link)
        {
            if (name is null)
            {
                return $"{nameof(SocialNetwork)} " + $"{nameof(name)}" + " can not be null";
            }

            if(link is null)
            {
                return $"{nameof(SocialNetwork)} " + $"{nameof(link)}" + " can not be null";
            }

            var network = new SocialNetwork(name, link);

            return network;
        }
    }
}
