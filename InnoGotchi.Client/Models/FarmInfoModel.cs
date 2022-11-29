using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;

namespace InnoGotchi.Client.Models
{
    public class FarmInfoModel : IMapWith<FarmInfo>, IMapWith<CreateFarmDto>
    {
        public string Name { get; set; } = "";
        public string CountAlivePets { get; set; } = "";
        public string CountDeadPets { get; set; } = "";
        public string AverageFeedingPeriod { get; set; } = "";
        public string AverageThirstQuenchingPeriod { get; set; } = "";
        public string AveragePetsHappinessDaysCount { get; set; } = "";
        public string AveragePetsAgeCount { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FarmInfo, FarmInfoModel>()
            .ForMember(userCommand => userCommand.Name,
            opt => opt.MapFrom(userDto => userDto.Name))
            .ForMember(userCommand => userCommand.CountAlivePets,
            opt => opt.MapFrom(userDto => $"{userDto.CountAlivePets}"))
            .ForMember(userCommand => userCommand.CountDeadPets,
            opt => opt.MapFrom(userDto => $"{userDto.CountDeadPets}"))
            .ForMember(userCommand => userCommand.AverageFeedingPeriod,
            opt => opt.MapFrom(userDto => $"{userDto.AverageFeedingPeriod}"))
            .ForMember(userCommand => userCommand.AverageThirstQuenchingPeriod,
            opt => opt.MapFrom(userDto => $"{userDto.AverageThirstQuenchingPeriod}"))
            .ForMember(userCommand => userCommand.AveragePetsHappinessDaysCount,
            opt => opt.MapFrom(userDto => $"{userDto.AveragePetsHappinessDaysCount}"))
            .ForMember(userCommand => userCommand.AveragePetsAgeCount,
            opt => opt.MapFrom(userDto => $"{userDto.AveragePetsAgeCount}"));

            profile.CreateMap<FarmInfoModel, CreateFarmDto>()
            .ForMember(userCommand => userCommand.Name,
            opt => opt.MapFrom(userDto => userDto.Name));

        }

    }
}
