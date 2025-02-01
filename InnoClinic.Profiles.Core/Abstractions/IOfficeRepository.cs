using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Core.Abstractions
{
    public interface IOfficeRepository : IRepositoryBase<OfficeModel>
    {
        Task<OfficeModel> GetByIdAsync(Guid id);
    }
}
