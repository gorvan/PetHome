using Microsoft.AspNetCore.Mvc;
using Minio;
using PetHome.API.Extensions;
using PetHome.Application.Pets.AddFiles;
using PetHome.Application.Pets.Delete;
using PetHome.Application.Pets.GetFile;
using PetHome.Application.Pets.GetFiles;

namespace PetHome.API.Controllers
{
    public class FileController : BaseContoller
    {
        private readonly IMinioClient _minioClient;
        public FileController(
            ILogger<FileController> logger,
            IMinioClient minioClient)
            : base(logger)
        {
            _minioClient = minioClient;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateFile(
            [FromServices] AddFilesHandler handler,
            IFormFile file,
            string bucketName,
            CancellationToken token
            )
        {
            using var stream = file.OpenReadStream();

            var filePath = Guid.NewGuid().ToString();

            var command = new AddFileCommand(stream, filePath, bucketName);

            _logger.LogInformation("Add file request");

            var result = await handler.Execute(command, token);

            return result.ToResponse();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<string>> GetFile(
            [FromServices] GetFileHandler handler,
            [FromRoute] Guid id,
            string bucketName,
            CancellationToken token
            )
        {
            var filePath = id.ToString();

            var command = new GetFileCommand(filePath, bucketName);

            _logger.LogInformation("Get file request");

            var result = await handler.Execute(command, token);

            return result.ToResponse();
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetFiles(
            [FromServices] GetFilesHandler handler,            
            string bucketName,
            string filePrefix,
            CancellationToken token
            )
        {
            var command = new GetFilesCommand(filePrefix, bucketName);

            _logger.LogInformation("Add files request");

            var result = await handler.Execute(command, token);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteFile(
           [FromServices] DeleteFileHandler handler,
           [FromRoute] Guid id,
           string bucketName,
           CancellationToken token
           )
        {
            var filePath = id.ToString();

            var command = new DeleteFileCommand(filePath, bucketName);

            _logger.LogInformation("Delete file request");

            var result = await handler.Execute(command, token);

            return result.ToResponse();
        }
    }
}
