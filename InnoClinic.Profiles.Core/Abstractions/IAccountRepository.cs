using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Core.Abstractions
{
    public interface IAccountRepository : IRepositoryBase<AccountEntity>
    {
        Task<AccountEntity> GetByIdAsync(Guid id);
    }
}
