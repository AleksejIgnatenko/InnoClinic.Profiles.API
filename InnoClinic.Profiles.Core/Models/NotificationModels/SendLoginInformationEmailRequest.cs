namespace InnoClinic.Profiles.Core.Models.NotificationModels
{
    public record SendLoginInformationEmailRequest(
        string Email,
        string EncryptedPassword,
        string FullName);
}
