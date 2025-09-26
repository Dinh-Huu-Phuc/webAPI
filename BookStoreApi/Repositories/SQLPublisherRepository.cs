using System.Collections.Generic;
using System.Linq;
using BookAPIStore.Models.DTO;
using BookAPIStore.Models.Domain;
using WebAPI.Data;

namespace BookAPIStore.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public SQLPublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- Bài 3: Check trùng tên (dành cho Controller gọi) ---
        public bool ExistsByName(string name)
            => _context.Publishers.Any(p => p.Name == name);

        public bool ExistsByNameExcludingId(string name, int excludeId)
            => _context.Publishers.Any(p => p.Name == name && p.Id != excludeId);

        public bool HasBooks(int publisherId)
            => _context.Books.Any(b => b.PublisherID == publisherId);

        // --- GET ALL ---
        public List<PublisherDTO> GetAllPublishers()
        {
            return _context.Publishers
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        // --- GET BY ID ---
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null!;

            return new PublisherNoIdDTO
            {
                Name = publisher.Name
            };
        }

        // --- ADD ---
        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var entity = new Publishers
            {
                Name = addPublisherRequestDTO.Name
            };

            _context.Publishers.Add(entity);
            _context.SaveChanges();

            return new AddPublisherRequestDTO
            {
                Name = entity.Name
            };
        }

        // --- UPDATE ---
        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var entity = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (entity == null) return null!;

            entity.Name = publisherNoIdDTO.Name;
            _context.SaveChanges();

            return new PublisherNoIdDTO
            {
                Name = entity.Name
            };
        }

        // --- DELETE ---
        public Publishers DeletePublisherById(int id)
        {
            var entity = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (entity == null) return null!;

            _context.Publishers.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
