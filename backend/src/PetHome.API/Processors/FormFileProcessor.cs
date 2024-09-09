using PetHome.Application.Volunteers.Shared;

namespace PetHome.API.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileDto> _filesDto = [];

        public List<FileDto> Process(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();

                var fileDto = FileDto.Create(
                        stream,
                        file.FileName,
                        file.ContentType);

                _filesDto.Add(fileDto);
            }
            return _filesDto;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var file in _filesDto)
            {
                await file.Stream.DisposeAsync();
            }
        }
    }
}
