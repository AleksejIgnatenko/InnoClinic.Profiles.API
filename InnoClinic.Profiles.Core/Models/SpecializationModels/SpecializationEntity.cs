namespace InnoClinic.Profiles.Core.Models.SpecializationModels
{
    public class SpecializationEntity
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
