using AutoMapper;
using InnoClinic.Profiles.Core.Models.PatientModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<PatientEntity, PatientDto>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Id));
            CreateMap<PatientDto, PatientEntity>();
        }
    }
}
