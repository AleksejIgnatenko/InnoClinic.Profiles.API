using AutoMapper;
using InnoClinic.Profiles.Core.Models.SpecializationModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class SpecializationMappingProfile : Profile
    {
        public SpecializationMappingProfile()
        {
            CreateMap<SpecializationDto, SpecializationEntity>();
            CreateMap<SpecializationEntity, SpecializationDto>();
        }
    }
}