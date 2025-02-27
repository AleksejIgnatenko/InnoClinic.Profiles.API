using InnoClinic.Profiles.Core.Models.AccountModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(AccountEntity account, string fullName);
    }
}