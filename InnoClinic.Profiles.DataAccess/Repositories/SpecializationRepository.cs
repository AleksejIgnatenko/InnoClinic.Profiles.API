using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.SpecializationModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class SpecializationRepository : RepositoryBase<SpecializationEntity>, ISpecializationRepository
    {
        public SpecializationRepository(InnoClinicProfilesDbContext context) : base(context) { }

        public async Task<SpecializationEntity> GetByIdAsync(Guid id)
        {
            return await _context.Specializations
                .FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new DataRepositoryException("Specialization not found", 404);
        }

        public override async Task UpdateAsync(SpecializationEntity entity)
        {
            await _context.Specializations
                .Where(s => s.Id.Equals(entity.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.SpecializationName, entity.SpecializationName)
                    .SetProperty(s => s.IsActive, entity.IsActive)
                );
        }

        public override async Task DeleteAsync(SpecializationEntity entity)
        {
            await _context.Specializations
                .Where(o => o.Id.Equals(entity.Id))
                .ExecuteDeleteAsync();
        }
    }
}
