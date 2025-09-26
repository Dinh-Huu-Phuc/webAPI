using BookAPIStore.Models.Domain;
using BookAPIStore.Models.DTO;

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
