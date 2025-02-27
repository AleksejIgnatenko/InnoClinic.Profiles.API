using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.OfficeModels;

namespace InnoClinic.Profiles.Core.Models.ReceptionistModels
{
    public class ReceptionistEntity
    {
        public Guid Id { get; set; }
        public AccountEntity Account { get; set; } = new AccountEntity();
        public OfficeEntity Office { get; set; } = new OfficeEntity();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
