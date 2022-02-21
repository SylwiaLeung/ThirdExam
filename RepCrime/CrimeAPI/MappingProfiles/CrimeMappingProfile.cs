using AutoMapper;
using CrimeService.Models;
using CrimeService.Models.Dtos;
using EventBus.Messaging.Events;

namespace CrimeService.MappingProfiles
{
    public class CrimeMappingProfile : Profile
    {
        public CrimeMappingProfile()
        {
            CreateMap<CrimeCreateDto, Crime>();
            CreateMap<Crime, CrimeReadDto>();
            CreateMap<Crime, CrimeEvent>();
        }
    }
}
