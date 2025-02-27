using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class DoctorRepository : RepositoryBase<DoctorEntity>, IDoctorRepository
    {
        public DoctorRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllAsync()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllAtWorkAsync()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Status.Equals("At work"))
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .ToListAsync();
        }

        public async Task<DoctorEntity> GetByAccountId(Guid accountId)
        {
            return await _context.Doctors
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .FirstOrDefaultAsync(d => d.Account.Id.Equals(accountId))
                ?? throw new DataRepositoryException($"Doctor with accountId '{accountId}' not found.", StatusCodes.Status404NotFound);
        }

        public async Task<DoctorEntity> GetByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .FirstOrDefaultAsync(d => d.Id.Equals(id))
                ?? throw new DataRepositoryException($"Doctor with Id '{id}' not found.", StatusCodes.Status404NotFound); 
        }

        public async override Task UpdateAsync(DoctorEntity entity)
        {
            var doctor = await GetByIdAsync(entity.Id);

            doctor.FirstName = entity.FirstName;
            doctor.LastName = entity.LastName;
            doctor.MiddleName = entity.MiddleName;
            doctor.CabinetNumber = entity.CabinetNumber;
            doctor.DateOfBirth = entity.DateOfBirth;
            doctor.Specialization = entity.Specialization;
            doctor.Office = entity.Office;
            doctor.CareerStartYear = entity.CareerStartYear;
            doctor.Status = entity.Status;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid officeId, string status)
        {
            await _context.Doctors
                .Where(d => d.Office.Id == officeId)
                .ExecuteUpdateAsync(d => d
                    .SetProperty(d => d.Status, status)
                );
        }
    }
}
