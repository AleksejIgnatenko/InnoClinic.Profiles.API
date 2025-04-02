using InnoClinic.Profiles.Core.Models.PatientModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IPatientService
    {
        Task CreatePatientAsync(string firstName, string lastName, string middleName, string dateOfBirth);
        Task CreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber, bool isLinkedToAccount, string dateOfBirth, string? photoId, string token);
        Task DeletePatientAsync(Guid id);
        Task<IEnumerable<PatientEntity>> GetAllPatientsAsync();
        Task UpdatePatientAsync(Guid id, string firstName, string lastName, string middleName, bool isLinkedToAccount, string dateOfBirth, string? photoId);
        Task AccountConnectionWithThePatient(string token, Guid patientId);
        Task ForceCreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber, bool isLinkedToAccount, string dateOfBirth, string? photoId, string token);
        Task<PatientEntity> GetPatientByAccountIdFromTokenAsync(string token);
        Task<PatientEntity> GetPatientByIdAsync(Guid patientId);
    }
}