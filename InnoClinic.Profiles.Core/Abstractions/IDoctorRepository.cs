using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.DoctorModels;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface IDoctorRepository : IRepositoryBase<DoctorEntity>
    {
        Task<IEnumerable<DoctorEntity>> GetAllAsync();
        Task<IEnumerable<DoctorEntity>> GetAllAtWorkAsync();
        Task<DoctorEntity> GetByIdAsync(Guid id);
        Task<DoctorEntity> GetByAccountId(Guid accountId);
        Task UpdateAsync(Guid officeId, string status);
    }
}