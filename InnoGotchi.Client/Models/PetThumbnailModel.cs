using AutoMapper;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.Domain;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace InnoGotchi.Client.Models
{
    public class PetThumbnailModel : IMapWith<Pet>
    {
        public string BackgroundColor { get; set; }
        public string BodyPath { get; set; }
        public string EyesPath { get; set; }
        public string MouthPath { get; set; }
        public string NosePath { get; set; }
        public string PetName { get; set; }
        public int Hungry { get; set; }
        public int Thirsty { get; set; }
        public uint Age { get; set; }
        public uint Happy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pet, PetThumbnailModel>()
                .ForMember(thumbnail => thumbnail.PetName,
                opt => opt.MapFrom(pet => pet.Name))
                .ForMember(thumbnail => thumbnail.NosePath,
                opt => opt.MapFrom(pet => pet.Appearance.Nose.Path))
                .ForMember(thumbnail => thumbnail.MouthPath,
                opt => opt.MapFrom(pet => pet.Appearance.Mouth.Path))
                .ForMember(thumbnail => thumbnail.EyesPath,
                opt => opt.MapFrom(pet => pet.Appearance.Eye.Path))
                .ForMember(thumbnail => thumbnail.BodyPath,
                opt => opt.MapFrom(pet => pet.Appearance.Body.Path))
                .ForMember(thumbnail => thumbnail.BackgroundColor,
                opt => opt.MapFrom(pet => pet.IsAlive ? "Aqua" : "Brown"))
                .ForMember(thumbnail => thumbnail.Hungry,
                opt => opt.MapFrom(pet => (int)Math.Round(pet.Status.HungerLevel)))
                .ForMember(thumbnail => thumbnail.Thirsty,
                opt => opt.MapFrom(pet => (int)Math.Round(pet.Status.ThirstyLevel)))
                .ForMember(thumbnail => thumbnail.Hungry,
                opt => opt.MapFrom(pet => (int)Math.Round(pet.Status.HungerLevel)))
                .ForMember(thumbnail => thumbnail.Age,
                opt => opt.MapFrom(pet => pet.Status.Age))
                .ForMember(thumbnail => thumbnail.Happy,
                opt => opt.MapFrom(pet => pet.Status.HappinessDayCount));




        }

    }
}
