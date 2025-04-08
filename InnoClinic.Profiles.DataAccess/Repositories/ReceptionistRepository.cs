using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class ReceptionistRepository : RepositoryBase<ReceptionistEntity>, IReceptionistRepository
    {
        public ReceptionistRepository(InnoClinicProfilesDbContext context) : base(context)
        {
        }

        public async Task<ReceptionistEntity> GetByIdAsync(Guid id)
        {
            return await _context.Receptionists
                .Include(r => r.Account)
                .Include(r => r.Office)
                .FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new DataRepositoryException("Receptionist not found", 404);
        }

        public async Task<IEnumerable<ReceptionistEntity>> GetAllAsync()
        {
            return await _context.Receptionists
                .AsNoTracking()
                .Include(r => r.Account)
                .Include(r => r.Office)
                .ToListAsync();
        }

        public async Task<ReceptionistEntity> GetByAccountId(Guid accountId)
        {
            return await _context.Receptionists
                .Include(r => r.Account)
                .Include(r => r.Office)
                .FirstOrDefaultAsync(r => r.Account.Id == accountId)
                ?? throw new DataRepositoryException($"Receptionist with accountId '{accountId}' not found.", StatusCodes.Status404NotFound);
        }

        public async override Task UpdateAsync(ReceptionistEntity entity)
        {
            var receptionist = await GetByIdAsync(entity.Id);

            receptionist.FirstName = entity.FirstName;
            receptionist.LastName = entity.LastName;
            receptionist.MiddleName = entity.MiddleName;
            receptionist.Office = entity.Office;
            receptionist.Status = entity.Status;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid officeId, string status)
        {
            await _context.Receptionists
                .Where(r => r.Office.Id == officeId)
                .ExecuteUpdateAsync(d => d
                    .SetProperty(d => d.Status, status)
                );
        }
    }
}
