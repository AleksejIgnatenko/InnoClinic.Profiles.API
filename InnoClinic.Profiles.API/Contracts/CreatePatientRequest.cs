namespace InnoClinic.Profiles.API.Contracts
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
