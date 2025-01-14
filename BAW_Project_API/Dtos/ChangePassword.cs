namespace BAW_Project_API.Dtos
{
    public class ChangePassword
    {
        public string Username { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }
}
