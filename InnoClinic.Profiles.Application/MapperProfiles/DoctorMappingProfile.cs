using AutoMapper;
using InnoClinic.Profiles.Core.Models.DoctorModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<DoctorEntity, DoctorDto>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Id));
            CreateMap<DoctorDto, DoctorEntity>();
        }
    }
}
