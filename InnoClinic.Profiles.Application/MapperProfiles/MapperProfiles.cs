using AutoMapper;
using InnoClinic.Profiles.Core.Dto;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<AccountDto, AccountModel>();
            CreateMap<AccountModel, AccountDto>();

            CreateMap<OfficeDto, OfficeModel>();

            CreateMap<SpecializationDto, SpecializationModel>();

            CreateMap<DoctorModel, DoctorDto>();
            CreateMap<DoctorDto, DoctorModel>();

            CreateMap<PatientModel, PatientDto>();
            CreateMap<PatientDto, PatientModel>();
        }
    }
}
