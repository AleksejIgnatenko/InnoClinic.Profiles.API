using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IValidationService
    {
        Dictionary<string, string> Validation(AccountModel accountModel);
        Dictionary<string, string> Validation(DoctorModel doctorModel);
        Dictionary<string, string> Validation(PatientModel patientModel);
    }
}