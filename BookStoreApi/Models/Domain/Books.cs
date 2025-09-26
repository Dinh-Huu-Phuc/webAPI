using System.ComponentModel.DataAnnotations;

namespace BookAPIStore.Models.Domain
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}\p{N}\s]+$", ErrorMessage = "Title chỉ được chứa chữ, số và khoảng trắng.")]
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //Navigation propperties - One Publisher has many book
        [Required]
        public int PublisherID { get; set; }
        public Publishers Publisher { get; set; }
        //navigation Propperties- One Book has many book_author
        public List<Book_Authors> Book_Author { get; set; }
    }
}
