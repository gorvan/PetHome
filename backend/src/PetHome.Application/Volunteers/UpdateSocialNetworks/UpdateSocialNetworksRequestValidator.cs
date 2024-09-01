using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.UpdateSocialNetworks
{
    public class UpdateSocialNetworksRequestValidator 
        : AbstractValidator<UpdateSocialNetworksRequest>
    {
        public UpdateSocialNetworksRequestValidator()
        {
            RuleFor(ur => ur.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleForEach(c => c.SocialNetworks)
                .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Path));
        }
    }
}
