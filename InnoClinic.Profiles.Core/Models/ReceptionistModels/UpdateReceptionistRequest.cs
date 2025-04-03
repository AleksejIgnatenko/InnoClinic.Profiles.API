namespace InnoClinic.Profiles.Core.Models.ReceptionistModels
{
    public record UpdateReceptionistRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string Status,
        Guid OfficeId,
        string PhotoId
        );
}
