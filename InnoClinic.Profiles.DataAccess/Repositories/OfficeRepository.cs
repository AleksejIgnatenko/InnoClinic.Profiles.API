using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.OfficeModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class OfficeRepository : RepositoryBase<OfficeEntity>, IOfficeRepository
    {
        public OfficeRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<OfficeEntity> GetByIdAsync(Guid id)
        {
            return await _context.Offices
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new DataRepositoryException("Office not found", 404);
        }

        public override async Task UpdateAsync(OfficeEntity entity)
        {
            await _context.Offices
                .Where(o => o.Id.Equals(entity.Id))
                .ExecuteUpdateAsync(o => o
                    .SetProperty(o => o.City, entity.City)
                    .SetProperty(o => o.Street, entity.Street)
                    .SetProperty(o => o.HouseNumber, entity.HouseNumber)
                    .SetProperty(o => o.OfficeNumber, entity.OfficeNumber)
                    .SetProperty(o => o.RegistryPhoneNumber, entity.RegistryPhoneNumber)
                    .SetProperty(o => o.IsActive, entity.IsActive)
                );
        }

        public override async Task DeleteAsync(OfficeEntity entity)
        {
            await _context.Offices
                .Where(o => o.Id.Equals(entity.Id))
                .ExecuteDeleteAsync();
        }
    }
}
