namespace InnoClinic.Profiles.Core.Models.PatientModels
{
    public record UpdatePatientRequest(
            string FirstName,
            string LastName,
            string MiddleName,
            string PhoneNumber,
            bool IsLinkedToAccount,
            string DateOfBirth,
            string? PhotoId
            );
}
