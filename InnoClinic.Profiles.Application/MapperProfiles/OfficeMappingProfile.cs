using AutoMapper;
using InnoClinic.Profiles.Core.Models.OfficeModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class OfficeMappingProfile : Profile
    {
        public OfficeMappingProfile()
        {
            CreateMap<OfficeDto, OfficeEntity>();
            CreateMap<OfficeEntity, OfficeDto>();
        }
    }
}
