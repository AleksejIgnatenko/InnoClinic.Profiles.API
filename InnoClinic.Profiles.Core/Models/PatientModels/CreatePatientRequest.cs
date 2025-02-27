namespace InnoClinic.Profiles.Core.Models.PatientModels
{
    public record CreatePatientRequest(
            string FirstName,
            string LastName,
            string MiddleName,
            string PhoneNumber,
            bool IsLinkedToAccount,
            string DateOfBirth
            );
}
