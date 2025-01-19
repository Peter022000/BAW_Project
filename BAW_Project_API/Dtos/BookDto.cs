namespace BAW_Project_API.Dtos
{
    public class BookDto
    {
        public required string Title { get; set; } = string.Empty;
        public required int AuthorId { get; set; }
        public required int GenreId { get; set; }
    }
}
