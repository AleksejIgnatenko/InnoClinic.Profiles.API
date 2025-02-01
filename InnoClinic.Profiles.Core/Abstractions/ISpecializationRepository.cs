using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface ISpecializationRepository : IRepositoryBase<SpecializationModel>
    {
        Task<SpecializationModel> GetByIdAsync(Guid id);
    }
}