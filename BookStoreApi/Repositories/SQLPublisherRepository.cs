using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookAPIStore.Models.DTO;
using BookAPIStore.Models.Domain;        
using WebAPI.Data;
using BookAPIStore.Repositories;

namespace BookAPIStore.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public SQLPublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
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

        // GET BY ID
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null!; // giữ nguyên đúng kiểu trong PDF

            return new PublisherNoIdDTO
            {
                Name = publisher.Name
            };
        }

        // ADD
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
