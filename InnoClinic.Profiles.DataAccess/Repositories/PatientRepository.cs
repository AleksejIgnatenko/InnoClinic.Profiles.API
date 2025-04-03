using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.PatientModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class PatientRepository : RepositoryBase<PatientEntity>, IPatientRepository
    {
        public PatientRepository(InnoClinicProfilesDbContext context) : base(context) { }

        public async Task<IEnumerable<PatientEntity>> GetAllAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<PatientEntity> GetByIdAsync(Guid id)
        {
            return await _context.Patients
                .Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new DataRepositoryException("Patient not found", 404);
        }

        public async Task<IEnumerable<PatientEntity>> FindMatchingPatientsByCriteriaAsync(PatientEntity patient)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p =>
                    (p.FirstName.Equals(patient.FirstName) ? 5 : 0) +
                    (p.LastName.Equals(patient.LastName) ? 5 : 0) +
                    (p.MiddleName.Equals(patient.MiddleName) ? 5 : 0) +
                    (p.DateOfBirth.Equals(patient.DateOfBirth) ? 3 : 0) >= 13 &&
                    !p.IsLinkedToAccount
                ).ToListAsync();
        }

        public async Task<PatientEntity> GetByAccountId(Guid accountId)
        {
            return await _context.Patients
                .Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.Account.Id.Equals(accountId))
                ?? throw new DataRepositoryException($"Patient with {accountId} not found", 404);
        }
    }
}
