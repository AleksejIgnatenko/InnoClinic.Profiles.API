using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class SpecializationRepository : RepositoryBase<SpecializationModel>, ISpecializationRepository
    {
        public SpecializationRepository(InnoClinicProfilesDbContext context) : base(context) { }

        public async Task<SpecializationModel> GetByIdAsync(Guid id)
        {
            return await _context.Specializations
                .FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new DataRepositoryException("Specialization not found", 404);
        }
    }
}
