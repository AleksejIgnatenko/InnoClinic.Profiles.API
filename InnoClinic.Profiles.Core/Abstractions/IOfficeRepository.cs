using InnoClinic.Profiles.Core.Models.OfficeModels;

namespace InnoClinic.Profiles.Core.Abstractions
{
    public interface IOfficeRepository : IRepositoryBase<OfficeEntity>
    {
        Task<OfficeEntity> GetByIdAsync(Guid id);
    }
}
