namespace WebAPI.Models.Domain
{
    public class Book_Authors
    {
        public int Id { get; set; }

        // Foreign keys
        public int BookId { get; set; }
        public Books Book { get; set; }

        public int AuthorId { get; set; }
        public Authors Author { get; set; }
    }
}
