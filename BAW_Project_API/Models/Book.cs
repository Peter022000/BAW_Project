namespace BAW_Project_API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; } = string.Empty;

        public required int AuthorId { get; set; }
        public required Author Author { get; set; }

        public required int GenreId { get; set; }
        public required Genre Genre { get; set; }
    }
}
