using AutoMapper;
using CommonItems.Models;
using EventBus.Messaging.Events;
using LawEnforcement.Domain.DTO;
using LawEnforcement.Domain.Entities;

namespace LawEnforcement.Application.MappingProfiles
{
    public class EnforcementMappingProfile : Profile
    {
        public EnforcementMappingProfile()
        {
            CreateMap<Enforcement, EnforcementReadDto>().ReverseMap();
            CreateMap<EnforcementCreateDto, Enforcement>();
            CreateMap<CrimeUpdateEvent, CrimeEvent>().ReverseMap();
            CreateMap<CrimeUpdateDto, CrimeEvent>().ReverseMap();
            CreateMap<Enforcement, EnforcementStatsReadDto>()
                .ForMember(x => x.NumberOfCrimes, n => n.MapFrom(e => e.Crimes.Count));
        }
    }
}
