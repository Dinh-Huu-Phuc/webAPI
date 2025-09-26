using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AuthorDTO> GellAllAuthors()
        {
            var allAuthors = _dbContext.Authors.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName
            }).ToList();

            return allAuthors;
        }

        public AuthorNoIdDTO GetAuthorById(int id)
        {
            var authorWithDomain = _dbContext.Authors.Where(n => n.Id == id);
            var authorWithIdDTO = authorWithDomain.Select(author => new AuthorNoIdDTO
            {
                FullName = author.FullName
            }).FirstOrDefault();

            return authorWithIdDTO;
        }

        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomain = new Authors
            {
                FullName = addAuthorRequestDTO.FullName
            };

            _dbContext.Authors.Add(authorDomain);
            _dbContext.SaveChanges();

            return addAuthorRequestDTO;
        }

        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (authorDomain != null)
            {
                authorDomain.FullName = authorNoIdDTO.FullName;
                _dbContext.SaveChanges();
            }

            return authorNoIdDTO;
        }

        public Authors? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
                return authorDomain;
            }

            return null;
        }

        // ✅ Kiểm tra xem tác giả có được gán vào bất kỳ cuốn sách nào không
        public bool HasAnyBook(int authorId)
        {
            return _dbContext.Book_Authors.Any(ba => ba.AuthorId == authorId);
        }
    }
}
