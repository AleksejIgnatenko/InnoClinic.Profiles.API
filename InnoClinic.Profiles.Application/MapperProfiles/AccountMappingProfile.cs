using AutoMapper;
using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<AccountDto, AccountEntity>();
            CreateMap<AccountEntity, AccountDto>();
        }
    }
}
