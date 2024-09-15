using FluentValidation;
using PetHome.Application.Validation;
using PetHome.Domain.PetManadgement.ValueObjects;
using PetHome.Domain.Shared;

namespace PetHome.Application.Volunteers.UpdateSocialNetworks
{
    public class UpdateSocialNetworksCommandValidator 
        : AbstractValidator<UpdateSocialNetworksCommand>
    {
        public UpdateSocialNetworksCommandValidator()
        {
            RuleFor(ur => ur.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequeired());

            RuleForEach(c => c.SocialNetworks)
                .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Path));
        }
    }
}
