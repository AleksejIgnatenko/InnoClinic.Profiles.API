namespace InnoClinic.Profiles.Core.Models.DoctorModels
{
    public record UpdateDoctorRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        int CabinetNumber,
        string DateOfBirth,
        Guid SpecializationId,
        Guid OfficeId,
        string CareerStartYear,
        string Status
        );
}
