using AutoMapper;
using CrimeService.Models;
using CrimeService.Models.Dtos;

namespace CrimeService.MappingProfiles
{
    public class CrimeMappingProfile : Profile
    {
        public CrimeMappingProfile()
        {
            CreateMap<CrimeCreateDto, Crime>();
            CreateMap<Crime, CrimeReadDto>();
        }
    }
}
