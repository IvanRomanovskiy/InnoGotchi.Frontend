using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;
using System;

namespace InnoGotchi.Client.Models
{
    public class CreatePetModel : IMapWith<CreatePetDto>
    {
        public string Name { get; set; } = "";
        public PetAppearance Appearance { get; set; } = new PetAppearance();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePetModel, CreatePetDto>()
            .ForMember(userCommand => userCommand.PetName,
            opt => opt.MapFrom(userDto => userDto.Name))
            .ForMember(userCommand => userCommand.BodyPath,
            opt => opt.MapFrom(userDto => userDto.Appearance.BodyPath))
            .ForMember(userCommand => userCommand.EyePath,
            opt => opt.MapFrom(userDto => userDto.Appearance.EyesPath))
            .ForMember(userCommand => userCommand.MouthPath,
            opt => opt.MapFrom(userDto => userDto.Appearance.MouthPath))
            .ForMember(userCommand => userCommand.NosePath,
            opt => opt.MapFrom(userDto => userDto.Appearance.NosePath));
        }
    }
}
