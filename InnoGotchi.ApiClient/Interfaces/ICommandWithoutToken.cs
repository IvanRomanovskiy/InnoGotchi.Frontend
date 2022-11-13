using InnoGotchi.ApiClient.Models.Users;

namespace InnoGotchi.ApiClient.Interfaces
{
    public interface ICommandWithoutToken
    {
        public Task<bool> CreateUser(CreateUserDto createUser);
        public Task<string?> GetToken(GetTokenDto tokenDto);
    }
}
