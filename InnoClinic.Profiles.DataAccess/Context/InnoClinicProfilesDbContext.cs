using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using InnoClinic.Profiles.Core.Models.OfficeModels;
using InnoClinic.Profiles.Core.Models.PatientModels;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;
using InnoClinic.Profiles.Core.Models.SpecializationModels;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.DataAccess.Context
{
    public class InnoClinicProfilesDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<OfficeEntity> Offices { get; set; }
        public DbSet<SpecializationEntity> Specializations { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<ReceptionistEntity> Receptionists { get; set; }

        public InnoClinicProfilesDbContext(DbContextOptions<InnoClinicProfilesDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
