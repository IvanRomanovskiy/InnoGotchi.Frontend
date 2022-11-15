
using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Users;

namespace InnoGotchi.Client.Models
{
    public class LoginModel : IMapWith<GetTokenDto>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginModel, GetTokenDto>()
            .ForMember(userCommand => userCommand.Email,
            opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.Password,
            opt => opt.MapFrom(userDto => userDto.Password));
        }
    }
}
