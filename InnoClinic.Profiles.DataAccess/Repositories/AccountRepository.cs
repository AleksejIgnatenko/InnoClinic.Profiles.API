using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class AccountRepository : RepositoryBase<AccountModel>, IAccountRepository
    {
        public AccountRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<AccountModel> GetByIdAsync(Guid id)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new DataRepositoryException("Account not found", 404);
        }

        public override async Task UpdateAsync(AccountModel entity)
        {
            await _context.Accounts
                .Where(a => a.Id.Equals(entity.Id))
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.Email, entity.Email)
                    .SetProperty(a => a.PhoneNumber, entity.PhoneNumber)
                    .SetProperty(a => a.PhotoId, entity.PhotoId)
                );
        }

        public override async Task DeleteAsync(AccountModel entity)
        {
            await _context.Accounts
                .Where(a => a.Id.Equals(entity.Id))
                .ExecuteDeleteAsync();
        }
    }
}
