namespace BAW_Project_API.Dtos
{
    public class ChangePassword
    {
        public required string Username { get; set; } = string.Empty;
        public required string OldPassword { get; set; } = string.Empty;
        public required string NewPassword { get; set; } = string.Empty;

    }
}
