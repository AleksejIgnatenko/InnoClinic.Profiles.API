using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using InnoClinic.Profiles.Core.Models.PatientModels;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IValidationService
    {
        Dictionary<string, string> Validation(AccountEntity accountModel);
        Dictionary<string, string> Validation(DoctorEntity doctorModel);
        Dictionary<string, string> Validation(PatientEntity patientModel);
        Dictionary<string, string> Validation(ReceptionistEntity receptionistModel);
    }
}