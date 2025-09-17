using System.Collections.Generic;
using System.Linq;
using WebAPI.Data;                          
using BookAPIStore.Models.Domain;
using BookAPIStore.Models.DTO;


namespace BookAPIStore.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public SQLAuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public List<AuthorDTO> GellAllAuthors()
        {
            return _context.Authors
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FullName = a.FullName
                })
                .ToList();
        }

        // GET BY ID
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            var entity = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (entity == null) return null!;

            return new AuthorNoIdDTO
            {
                FullName = entity.FullName
            };
        }

        // ADD
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var entity = new Authors
            {
                FullName = addAuthorRequestDTO.FullName
            };

            _context.Authors.Add(entity);
            _context.SaveChanges();

            // Theo PDF, method trả về AddAuthorRequestDTO
            return new AddAuthorRequestDTO
            {
                FullName = entity.FullName
            };
        }

        // UPDATE BY ID
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var entity = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (entity == null) return null!;

            entity.FullName = authorNoIdDTO.FullName;
            _context.SaveChanges();

            return new AuthorNoIdDTO
            {
                FullName = entity.FullName
            };
        }

        // DELETE BY ID
        public Authors? DeleteAuthorById(int id)
        {
            var entity = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (entity == null) return null;

            _context.Authors.Remove(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
