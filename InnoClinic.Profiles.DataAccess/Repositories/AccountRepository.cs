using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class AccountRepository : RepositoryBase<AccountEntity>, IAccountRepository
    {
        public AccountRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<AccountEntity> GetByIdAsync(Guid id)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new DataRepositoryException("Account not found", 404);
        }

        public override async Task UpdateAsync(AccountEntity entity)
        {
            await _context.Accounts
                .Where(a => a.Id.Equals(entity.Id))
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.Email, entity.Email)
                    .SetProperty(a => a.PhoneNumber, entity.PhoneNumber)
                );
        }

        public override async Task DeleteAsync(AccountEntity entity)
        {
            await _context.Accounts
                .Where(a => a.Id.Equals(entity.Id))
                .ExecuteDeleteAsync();
        }
    }
}
