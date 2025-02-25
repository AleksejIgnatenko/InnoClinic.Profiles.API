using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(string firstName, string lastName, string middleName, int cabinetNumber, DateTime dateOfBirth, Guid accountId, Guid specializationId, Guid officeId, DateTime careerStartYear, string status);
        Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync();
        Task<IEnumerable<DoctorModel>> GetAllDoctorsAtWorkAsync();
        Task UpdateDoctorAsync(Guid id, string firstName, string lastName, string middleName, int cabinetNumber, DateTime dateOfBirth, Guid accountId, Guid specializationId, Guid officeId, DateTime careerStartYear, string status);
        Task DeleteDoctorAsync(Guid id);
    }
}