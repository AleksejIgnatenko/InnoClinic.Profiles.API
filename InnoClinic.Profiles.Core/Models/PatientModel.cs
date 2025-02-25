namespace InnoClinic.Profiles.Core.Models
{
    public class PatientModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public bool IsLinkedToAccount { get; set; }
        public string DateOfBirth { get; set; } = string.Empty;
        public AccountModel Account { get; set; } = new AccountModel();
    }
}
