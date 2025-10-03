using BookStoreApi.Models.Domain;
using WebAPI.Data;

namespace BookStoreApi.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
                                    IHttpContextAccessor httpContextAccessor,
                                    AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        // Service Upload
        public Image Upload(Image image)
        {
            // ensure Images folder exists
            var imagesFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            var fileNameOnly = Path.GetFileNameWithoutExtension(image.File.FileName);
            var extension = Path.GetExtension(image.File.FileName);
            var uniqueFileName = $"{fileNameOnly}_{Guid.NewGuid()}{extension}";

            var physicalPath = Path.Combine(imagesFolder, uniqueFileName);

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                image.File.CopyTo(stream);
            }

            // build URL so client can access: https://host/Images/uniqueFileName
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                              $"{_httpContextAccessor.HttpContext.Request.Host}" +
                              $"{_httpContextAccessor.HttpContext.Request.PathBase}" +
                              $"/Images/{uniqueFileName}";

            image.FileName = Path.GetFileNameWithoutExtension(uniqueFileName);
            image.FileExtension = extension;
            image.FileSizeInBytes = image.File.Length;
            image.FilePath = urlFilePath;

            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return image;
        }

        // Service Get info
        public List<Image> GetAllInfoImages()
        {
            var allImages = _dbContext.Images.ToList();
            return allImages;
        }

        // Service Download file
        public (byte[], string, string) DownloadFile(int Id)
        {
            try
            {
                var fileById = _dbContext.Images.Where(x => x.Id == Id).FirstOrDefault();
                if (fileById == null) return (null, null, null);

                // physical path: ContentRootPath/Images/fileName + extension
                var physicalPath = Path.Combine(_webHostEnvironment.ContentRootPath,
                                                "Images",
                                                $"{fileById.FileName}{fileById.FileExtension}");

                if (!File.Exists(physicalPath)) return (null, null, null);

                var bytes = File.ReadAllBytes(physicalPath);
                var fileName = fileById.FileName + fileById.FileExtension;

                var mime = "application/octet-stream";
                var ext = (fileById.FileExtension ?? "").ToLower();
                if (ext == ".jpg" || ext == ".jpeg") mime = "image/jpeg";
                else if (ext == ".png") mime = "image/png";
                else if (ext == ".gif") mime = "image/gif";
                else if (ext == ".pdf") mime = "application/pdf";

                return (bytes, fileName, mime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
