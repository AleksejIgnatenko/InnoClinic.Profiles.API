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
                ?? throw new DataRepositoryException("Office not found", 404);
        }
    }
}
