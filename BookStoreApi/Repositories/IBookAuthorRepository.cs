
using BookAPIStore.Models.DTO;
using WebAPI.Models.Domain;

namespace BookAPIStore.Repositories
{
    public interface IBookAuthorRepository
    {
        bool BookExists(int bookId);
        bool AuthorExists(int authorId);
        bool RelationExists(int bookId, int authorId);
        Book_Authors AddRelation(AddBookAuthorRequestDTO dto);
    }
}
