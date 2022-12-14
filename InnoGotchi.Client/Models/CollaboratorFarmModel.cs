using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Client.Components;
using InnoGotchi.Domain;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.Models
{
    public class UserFarmModel : IMapWith<CollaboratorFarm>, IMapWith<AllFarmsVm>
    {
        public string FarmName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public BitmapFrame OwnerAvatar { get; set; }
        public ICollection<Pet> Pets { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CollaboratorFarm, UserFarmModel>()
            .ForMember(userCommand => userCommand.FarmName,
            opt => opt.MapFrom(userDto => userDto.FarmName))
            .ForMember(userCommand => userCommand.OwnerFirstName,
            opt => opt.MapFrom(userDto => userDto.OwnerFirstName))
            .ForMember(userCommand => userCommand.OwnerLastName,
            opt => opt.MapFrom(userDto => userDto.OwnerLastName))
            .ForMember(userCommand => userCommand.OwnerAvatar,
            opt => opt.MapFrom(userDto => userDto.OwnerAvatarBase.ToImageFromBase64()))
            .ForMember(userCommand => userCommand.Pets,
            opt => opt.MapFrom(userDto => userDto.Pets));

            profile.CreateMap<AllFarmsVm, UserFarmModel>()
            .ForMember(userCommand => userCommand.FarmName,
            opt => opt.MapFrom(userDto => userDto.FarmName))
            .ForMember(userCommand => userCommand.OwnerFirstName,
            opt => opt.MapFrom(userDto => userDto.OwnerFirstName))
            .ForMember(userCommand => userCommand.OwnerLastName,
            opt => opt.MapFrom(userDto => userDto.OwnerLastName))
            .ForMember(userCommand => userCommand.OwnerAvatar,
            opt => opt.MapFrom(userDto => userDto.OwnerAvatarBase.ToImageFromBase64()))
            .ForMember(userCommand => userCommand.Pets,
            opt => opt.MapFrom(userDto => userDto.Pets));
        }

    }
}
