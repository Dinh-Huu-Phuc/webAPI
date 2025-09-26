using System.ComponentModel.DataAnnotations;

namespace BookAPIStore.Models.DTO
{
    public class AddBookAuthorRequestDTO
    {
        [Required] public int BookId { get; set; }
        [Required] public int AuthorId { get; set; }
    }
}
