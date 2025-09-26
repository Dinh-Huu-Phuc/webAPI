using System.ComponentModel.DataAnnotations;

namespace BookAPIStore.Models.Domain
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Tên tác giả tối thiểu 3 ký tự.")]
        public string FullName { get; set; }
        // Navigation properties - One author has many Book_author
        public List<Book_Authors> Book_Author { get; set; }
    }
}
