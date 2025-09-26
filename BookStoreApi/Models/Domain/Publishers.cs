using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookAPIStore.Models.Domain
{
    [Index(nameof(Name), IsUnique = true)] 
    public class Publishers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;
        //Navigation properties - One author has many book
        public List<Books> Books { get; set; }
    }
}
