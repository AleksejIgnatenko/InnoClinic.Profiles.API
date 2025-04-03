using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface IReceptionistRepository : IRepositoryBase<ReceptionistEntity>
    {
        Task<ReceptionistEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<ReceptionistEntity>> GetAllAsync();
        Task<ReceptionistEntity> GetByAccountId(Guid accountId);
        Task UpdateAsync(Guid officeId, string status);
    }
}