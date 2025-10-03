using BookStoreApi.Models.Domain;
using BookStoreApi.Models.DTO;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // ------------------- Upload -------------------
        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription
                };

                // User repository to upload image
                _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        // ------------------- Get Info All Images -------------------
        [HttpGet]
        [Route("GetAllImages")]
        public IActionResult GetInfoAllImages()
        {
            var allImages = _imageRepository.GetAllInfoImages();
            return Ok(allImages);
        }

        // ------------------- Download -------------------
        [HttpGet]
        [Route("Download")]
        public IActionResult DownloadImage(int id)
        {
            var result = _imageRepository.DownloadFile(id);

            if (result.Item1 == null)
                return NotFound("File not found");

            // Thứ tự đúng: byte[], contentType, fileDownloadName
            return File(result.Item1, result.Item3, result.Item2);
        }


        // ------------------- Validate -------------------
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760) // 10MB
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload smaller file.");
            }
        }
    }
}
