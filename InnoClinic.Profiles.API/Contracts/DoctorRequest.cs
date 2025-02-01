namespace InnoClinic.Profiles.API.Contracts
{
    public record DoctorRequest(
        string FirstName, 
        string LastName, 
        string MiddleName, 
        int CabinetNumber,
        DateTime DateOfBirth,
        Guid AccountId, 
        Guid SpecializationId, 
        Guid OfficeId, 
        DateTime CareerStartYear, 
        string Status
        );
}
