using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.SpecializationModels;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface ISpecializationRepository : IRepositoryBase<SpecializationEntity>
    {
        Task<SpecializationEntity> GetByIdAsync(Guid id);
    }
}