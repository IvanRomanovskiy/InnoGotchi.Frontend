using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.User
{
    public interface IUserCommands
    {
        public Task<UserData?> GetUserData(string token);
        public Task<bool> ChangeName(ChangeNameDto changeName, string token);
        public Task<bool> ChangePassword(ChangePasswordDto changePassword, string token);
        public Task<bool> ChangeAvatar(ChangeAvatarDto changeAvatar, string token);
    }
}
