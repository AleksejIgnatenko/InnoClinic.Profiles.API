using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IPatientService
    {
        Task CreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber, bool isLinkedToAccount, string dateOfBirth, string token);
        Task DeletePatientAsync(Guid id);
        Task<IEnumerable<PatientModel>> GetAllPatientsAsync();
        Task UpdatePatientAsync(Guid id, string firstName, string lastName, string middleName, bool isLinkedToAccount, string dateOfBirth, Guid accountId);
        Task AccountConnectionWithThePatient(string token, Guid patientId);
        Task ForceCreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber, bool isLinkedToAccount, string dateOfBirth, string token);
    }
}