using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.PatientModels;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface IPatientRepository : IRepositoryBase<PatientEntity>
    {
        Task<IEnumerable<PatientEntity>> GetAllAsync();
        Task<PatientEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<PatientEntity>> FindMatchingPatientsByCriteriaAsync(PatientEntity patient);
        Task<PatientEntity> GetByAccountId(Guid accountId);
    }
}