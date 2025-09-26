
using BookAPIStore.Models.DTO;
using WebAPI.Data;
using WebAPI.Models.Domain;

namespace BookAPIStore.Repositories 
{
    public class SQLBookAuthorRepository : IBookAuthorRepository
    {
        private readonly AppDbContext _context;
        public SQLBookAuthorRepository(AppDbContext context) => _context = context;

        public bool BookExists(int bookId) => _context.Books.Any(b => b.Id == bookId);
        public bool AuthorExists(int authorId) => _context.Authors.Any(a => a.Id == authorId);
        public bool RelationExists(int bookId, int authorId)
            => _context.Book_Authors.Any(x => x.BookId == bookId && x.AuthorId == authorId);

        public Book_Authors AddRelation(AddBookAuthorRequestDTO dto)
        {
            var entity = new Book_Authors { BookId = dto.BookId, AuthorId = dto.AuthorId };
            _context.Book_Authors.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
