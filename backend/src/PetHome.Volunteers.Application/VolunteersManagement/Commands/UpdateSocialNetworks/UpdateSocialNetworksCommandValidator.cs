using FluentValidation;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Validation;
using PetHome.Volunteers.Domain.ValueObjects;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks
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
