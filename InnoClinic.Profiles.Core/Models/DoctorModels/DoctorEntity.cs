using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.OfficeModels;
using InnoClinic.Profiles.Core.Models.SpecializationModels;

namespace InnoClinic.Profiles.Core.Models.DoctorModels
{
    public class DoctorEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int CabinetNumber { get; set; }
        public string DateOfBirth { get; set; } = string.Empty;
        public AccountEntity Account { get; set; } = new AccountEntity();
        public SpecializationEntity Specialization { get; set; } = new SpecializationEntity();
        public OfficeEntity Office { get; set; } = new OfficeEntity();
        public string CareerStartYear { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
