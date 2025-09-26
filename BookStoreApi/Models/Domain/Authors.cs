using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Domain
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        // Many-to-many: Author <-> Book (via Book_Authors)
        public List<Book_Authors> Book_Author { get; set; }
    }
}
