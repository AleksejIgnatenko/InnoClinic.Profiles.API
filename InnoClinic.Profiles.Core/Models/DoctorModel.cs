namespace InnoClinic.Profiles.Core.Models
{
    public class DoctorModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int CabinetNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AccountModel Account { get; set; } = new AccountModel();
        public SpecializationModel Specialization { get; set; } = new SpecializationModel();
        public OfficeModel Office { get; set; } = new OfficeModel();
        public DateTime CareerStartYear { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
