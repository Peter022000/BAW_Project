namespace BAW_Project_API.Dtos
{
    public class Login
    {
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
