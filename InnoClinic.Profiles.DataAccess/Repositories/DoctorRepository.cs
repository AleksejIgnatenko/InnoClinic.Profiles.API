﻿using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Repositories
{
    public class DoctorRepository : RepositoryBase<DoctorModel>, IDoctorRepository
    {
        public DoctorRepository(InnoClinicProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<DoctorModel>> GetAllAsync()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorModel>> GetAllAtWorkAsync()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Status.Equals("At work"))
                .Include(d => d.Account)
                .Include(d => d.Specialization)
                .Include(d => d.Office)
                .ToListAsync();
        }

        public async Task<DoctorModel> GetByIdAsync(Guid id)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id.Equals(id))
                ?? throw new DataRepositoryException($"Doctor with Id '{id}' not found.", StatusCodes.Status404NotFound); ;
        }
    }
}
