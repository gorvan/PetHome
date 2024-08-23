﻿using PetHome.Domain.Shared;

namespace PetHome.Domain.PetManadgement.ValueObjects
{
    public record SocialNetworks
    {
        private SocialNetworks() { }
        private SocialNetworks(IEnumerable<SocialNetwork> socialNets)
        {
            _socialNets = socialNets.ToList();
        }

        private List<SocialNetwork> _socialNets { get; } = [];
        public IReadOnlyList<SocialNetwork> SocialNets => _socialNets;


        public static Result<SocialNetworks> Create(IEnumerable<SocialNetwork> socialNets)
        {
            var socialNet = new SocialNetworks(socialNets);

            return socialNet;
        }
    }
}
