using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            var allPublisher = _dbContext.Publishers.Select(publisher => new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            }).ToList();

            return allPublisher;
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisherWithDomain = _dbContext.Publishers.Where(n => n.Id == id);
            var publisherWithIdDTO = publisherWithDomain.Select(publisher => new PublisherNoIdDTO
            {
                Name = publisher.Name,
            }).FirstOrDefault();

            return publisherWithIdDTO;
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherDomain = new Publishers
            {
                Name = addPublisherRequestDTO.Name
            };

            _dbContext.Publishers.Add(publisherDomain);
            _dbContext.SaveChanges();

            return addPublisherRequestDTO;
        }

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherNoIdDTO.Name;
                _dbContext.SaveChanges();
            }

            return publisherNoIdDTO;
        }

        public Publishers? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
                return publisherDomain;
            }

            return null;
        }

        public bool ExistsByName(string name)
        {
            return _dbContext.Publishers.Any(p => p.Name.ToLower() == name.ToLower());
        }

        public bool ExistsById(int id)
        {
            return _dbContext.Publishers.Any(p => p.Id == id);
        }

        // ✅ Method bổ sung 1: Kiểm tra tên đã tồn tại nhưng loại trừ theo ID
        public bool ExistsByNameExcludingId(string name, int excludeId)
        {
            return _dbContext.Publishers.Any(p => p.Name.ToLower() == name.ToLower() && p.Id != excludeId);
        }

        // ✅ Method bổ sung 2: Kiểm tra publisher có sách nào không
        public bool HasBooks(int publisherId)
        {
            return _dbContext.Books.Any(book => book.PublisherID == publisherId);
        }
    }
}
