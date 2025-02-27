namespace InnoClinic.Profiles.Core.Models.ReceptionistModels
{
    public record CreateReceptionistRequest(
            string FirstName,
            string LastName,
            string MiddleName,
            string Email,
            string Status,
            Guid OfficeId
            );
}
