namespace BAW_Project_API.Dtos
{
    public class AuthorDto
    {
        public int? id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
    }
}
