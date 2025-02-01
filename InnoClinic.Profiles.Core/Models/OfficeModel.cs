namespace InnoClinic.Profiles.Core.Models
{
    public class OfficeModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
