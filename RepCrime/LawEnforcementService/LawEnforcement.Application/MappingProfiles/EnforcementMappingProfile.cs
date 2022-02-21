using AutoMapper;
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
        }
    }
}
