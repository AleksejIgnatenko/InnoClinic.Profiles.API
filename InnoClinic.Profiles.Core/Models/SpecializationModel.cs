namespace InnoClinic.Profiles.Core.Models
{
    public class SpecializationModel
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
