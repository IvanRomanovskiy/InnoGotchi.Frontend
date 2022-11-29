
namespace InnoGotchi.ApiClient.Models.Users
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
