using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Core.FileProvider;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Volunteers.Application.VolunteersManagement.Commands.PetManagement.SetMainPetPhoto
{
    public class SetMainPetPhotoHandler : ICommandHandler<SetMainPetPhotoCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<SetMainPetPhotoHandler> _logger;
        private readonly IValidator<SetMainPetPhotoCommand> _validator;

        public SetMainPetPhotoHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            ILogger<SetMainPetPhotoHandler> logger,
            IValidator<SetMainPetPhotoCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result> Execute(SetMainPetPhotoCommand command, CancellationToken token)
        {
            var validationResult = await _validator.ValidateAsync(command, token);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository
                .GetById(VolunteerId.Create(command.VolunteerId), token);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var result = volunteerResult.Value.SetMainPetPhoto(command.PetId, command.FilePath);
            if (result.IsFailure)
            {
                return result.Error;
            }

            await _volunteerRepository.Update(volunteerResult.Value, token);

            _logger.LogInformation("Pet photo: {command.FilePath} set main", command.FilePath);

            return Result.Success();
        }
    }
}
