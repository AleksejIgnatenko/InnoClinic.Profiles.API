namespace InnoClinic.Profiles.Core.Models.OfficeModels
{
    public class OfficeEntity
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string OfficeNumber { get; set; } = string.Empty;
        public Guid PhotoId { get; set; }
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
