using BookStoreApi.Models.Domain;

namespace BookStoreApi.Repositories
{
    public interface IImageRepository
    {
        Image Upload(Image image);
        List<Image> GetAllInfoImages();
        (byte[], string, string) DownloadFile(int Id);
    }
}
