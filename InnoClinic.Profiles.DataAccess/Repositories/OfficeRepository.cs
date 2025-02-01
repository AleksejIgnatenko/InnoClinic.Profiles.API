using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class OfficeRepository : RepositoryBase<OfficeModel>, IOfficeRepository
    {
        public OfficeRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<OfficeModel> GetByIdAsync(Guid id)
        {
            return await _context.Offices
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new DataRepositoryException("Office not found", 404);
        }
    }
}
