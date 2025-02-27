using InnoClinic.Profiles.Core.Models.DoctorModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(string firstName, string lastName, string middleName, int cabinetNumber, string dateOfBirth, string email, Guid specializationId, Guid officeId, string careerStartYear, string status);
        Task<IEnumerable<DoctorEntity>> GetAllDoctorsAsync();
        Task<IEnumerable<DoctorEntity>> GetAllDoctorsAtWorkAsync();
        Task UpdateDoctorAsync(Guid id, string firstName, string lastName, string middleName, int cabinetNumber, string dateOfBirth, Guid specializationId, Guid officeId, string careerStartYear, string status);
        Task DeleteDoctorAsync(Guid id);
        Task<DoctorEntity> GetDoctorByIdAsync(Guid id);
        Task<DoctorEntity> GetDoctorByAccountIdFromTokenAsync(string token);
    }
}