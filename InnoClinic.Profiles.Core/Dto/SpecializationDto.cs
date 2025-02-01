namespace InnoClinic.Profiles.Core.Dto
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
