namespace BAW_Project_API.Dtos
{
    public class BookDto
    {
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
