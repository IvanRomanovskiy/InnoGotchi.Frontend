using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Application.Models.Users.Queries.GetUserData;

namespace InnoGotchi.ApiClient.Interfaces.User
{
    public interface IUserCommands
    {
        public Task<UserData?> GetUserData();
        public Task<bool> ChangeName(ChangeNameDto changeName);
        public Task<bool> ChangePassword(ChangePasswordDto changePassword);
        public Task<bool> ChangeAvatar(ChangeAvatarDto changeAvatar);
    }
}
