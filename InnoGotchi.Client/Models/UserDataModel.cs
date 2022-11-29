using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.Client.Components;
using InnoGotchi.Domain;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.Models
{
    public class UserDataModel : IMapWith<UserData>
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public BitmapFrame Avatar { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserDataModel, UserData>()
            .ForMember(userCommand => userCommand.firstName,
            opt => opt.MapFrom(userDto => userDto.FirstName))
            .ForMember(userCommand => userCommand.lastName,
            opt => opt.MapFrom(userDto => userDto.LastName))
            .ForMember(userCommand => userCommand.email,
            opt => opt.MapFrom(userDto => userDto.Email))
            .ForMember(userCommand => userCommand.avatarBase,
            opt => opt.MapFrom(userDto => userDto.Avatar.ToBase64String()));

            profile.CreateMap<UserData, UserDataModel>()
            .ForMember(userCommand => userCommand.FirstName,
            opt => opt.MapFrom(userDto => userDto.firstName))
            .ForMember(userCommand => userCommand.LastName,
            opt => opt.MapFrom(userDto => userDto.lastName))
            .ForMember(userCommand => userCommand.Email,
            opt => opt.MapFrom(userDto => userDto.email))
            .ForMember(userCommand => userCommand.Avatar,
            opt => opt.MapFrom(userDto => userDto.avatarBase.ToImageFromBase64()));

        }
    }
}
