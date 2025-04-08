namespace InnoClinic.Profiles.Core.Models.PatientModels
{
    public record CreateAdminPatientRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string DateOfBirth
    );
}
