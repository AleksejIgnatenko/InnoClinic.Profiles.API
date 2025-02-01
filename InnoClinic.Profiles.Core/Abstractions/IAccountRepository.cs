using InnoClinic.Profiles.Core.Models;

namespace InnoClinic.Profiles.Core.Abstractions
{
    public interface IAccountRepository : IRepositoryBase<AccountModel>
    {
        Task<AccountModel> GetByIdAsync(Guid id);
    }
}
