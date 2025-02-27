using AutoMapper;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class ReceptionistMappingProfile : Profile
    {
        public ReceptionistMappingProfile()
        {
            CreateMap<ReceptionistEntity, ReceptionistDto>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Id));
            CreateMap<ReceptionistDto, ReceptionistEntity>();
        }
    }
}
