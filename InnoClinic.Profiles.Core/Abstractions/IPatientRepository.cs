using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface IPatientRepository : IRepositoryBase<PatientModel>
    {
        Task<IEnumerable<PatientModel>> GetAllAsync();
        Task<PatientModel> GetByIdAsync(Guid id);
        Task<IEnumerable<PatientModel>> FindMatchingPatientsByCriteriaAsync(PatientModel patient);
    }
}