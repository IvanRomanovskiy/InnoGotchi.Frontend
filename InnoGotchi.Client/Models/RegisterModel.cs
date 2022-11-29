using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Components;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.Models
{
    public class RegisterModel : IMapWith<CreateUserDto>, IMapWith<GetTokenDto>
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = ""; 
        public string Password { get; set; } = "";
        public BitmapFrame Avatar { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterModel, CreateUserDto>()
            .ForMember(userCommand => userCommand.FirstName,
            opt => opt.MapFrom(userDto => userDto.FirstName))
            .ForMember(userCommand => userCommand.LastName,
            opt => opt.MapFrom(userDto => userDto.LastName))
            .ForMember(userCommand => userCommand.Email,
            opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.Password,
            opt => opt.MapFrom(userDto => userDto.Password))
            .ForMember(userCommand => userCommand.AvatarBase,
            opt => opt.MapFrom(userDto => userDto.Avatar.ToBase64String()));

            profile.CreateMap<RegisterModel, GetTokenDto>()
            .ForMember(userCommand => userCommand.Email,
            opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.Password,
            opt => opt.MapFrom(userDto => userDto.Password));
        }


    }
}
