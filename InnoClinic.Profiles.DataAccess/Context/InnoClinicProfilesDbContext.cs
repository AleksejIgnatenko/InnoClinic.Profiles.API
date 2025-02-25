using InnoClinic.Profiles.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Context
{
    public class InnoClinicProfilesDbContext : DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<OfficeModel> Offices { get; set; }
        public DbSet<SpecializationModel> Specializations { get; set; }
        public DbSet<PatientModel> Patients { get; set; }

        public InnoClinicProfilesDbContext(DbContextOptions<InnoClinicProfilesDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
