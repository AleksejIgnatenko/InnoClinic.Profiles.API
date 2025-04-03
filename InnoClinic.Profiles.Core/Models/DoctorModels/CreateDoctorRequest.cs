namespace InnoClinic.Profiles.Core.Models.DoctorModels
{
    public record CreateDoctorRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        int CabinetNumber,
        string DateOfBirth,
        string Email,
        Guid SpecializationId,
        Guid OfficeId,
        string CareerStartYear,
        string Status,
        string? PhotoId
        );
}
