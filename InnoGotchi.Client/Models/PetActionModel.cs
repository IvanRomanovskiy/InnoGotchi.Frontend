using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.ApiClient.Models.Pets;
using System;

namespace InnoGotchi.Client.Models
{
    public class PetActionModel : IMapWith<ThirstQuenchingPetDto>, IMapWith<FeedingPetDto>
    {
        public Guid PetId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<PetActionModel, ThirstQuenchingPetDto>()
                .ForMember(thirst => thirst.PetId,
                opts => opts.MapFrom(petAction => petAction.PetId));
            profile.CreateMap<PetActionModel, FeedingPetDto>()
                .ForMember(feed => feed.PetId,
                opts => opts.MapFrom(petAction => petAction.PetId));
        }
    }
}
