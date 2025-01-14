namespace BAW_Project_API.Dtos
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; } = false;
        public string? Token { get; set; } = string.Empty;
        public string? Error { get; set; } = string.Empty;
    }
}
