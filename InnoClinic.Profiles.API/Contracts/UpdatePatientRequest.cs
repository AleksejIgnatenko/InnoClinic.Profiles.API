namespace InnoClinic.Profiles.API.Contracts
{
    public record UpdatePatientRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string PhoneNumber,
        bool IsLinkedToAccount,
        string DateOfBirth,
        Guid AccountId
        );
}
