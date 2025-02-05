using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public interface IDoctorRepository : IRepositoryBase<DoctorModel>
    {
        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<IEnumerable<DoctorModel>> GetAllAtWorkAsync();
        Task<DoctorModel> GetByIdAsync(Guid id);
    }
}