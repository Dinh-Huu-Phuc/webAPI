using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Domain
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // One Publisher has many Books
        public int PublisherID { get; set; }
        public Publishers Publisher { get; set; }

        // Many-to-many: Book <-> Author (via Book_Authors)
        public List<Book_Authors> Book_Author { get; set; }
    }
}
