namespace InnoClinic.Profiles.Core.Models.AccountModels
{
    public record AccountUpdatePhonePhotoDto (
        Guid Id,
        string PhoneNumber,
        string? PhotoId
        );
}
