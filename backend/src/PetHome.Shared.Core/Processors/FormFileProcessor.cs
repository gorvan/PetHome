using Microsoft.AspNetCore.Http;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Shared.Core.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileDto> _filesDto = [];

        public List<FileDto> Process(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();

                var fileDto = new FileDto(
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
