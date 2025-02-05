using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class PatientRepository : RepositoryBase<PatientModel>, IPatientRepository
    {
        public PatientRepository(InnoClinicProfilesDbContext context) : base(context) { }

        public async Task<IEnumerable<PatientModel>> GetAllAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<PatientModel> GetByIdAsync(Guid id)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new DataRepositoryException("Patient not found", 404);
        }

        public async Task<IEnumerable<PatientModel>> FindMatchingPatientsByCriteriaAsync(PatientModel patient)
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
    }
}
