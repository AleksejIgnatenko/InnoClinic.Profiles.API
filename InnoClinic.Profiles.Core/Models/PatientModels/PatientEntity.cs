using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Core.Models.PatientModels
{
    public class PatientEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public bool IsLinkedToAccount { get; set; }
        public string DateOfBirth { get; set; } = string.Empty;
        public AccountEntity? Account { get; set; }
    }
}
