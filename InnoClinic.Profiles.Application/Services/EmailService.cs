using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.NotificationModels;
using System.Text;
using System.Text.Json;


namespace InnoClinic.Profiles.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly HttpClient _httpClient;

        public EmailService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _httpClient = new HttpClient();
        }

        public async Task SendEmailAsync(AccountEntity account, string fullName)
        {
            var encryptedPassword = _encryptionService.EncryptData(account.Password);

            var sendLoginInformationEmailRequest =
                new SendLoginInformationEmailRequest(account.Email, encryptedPassword, fullName);

            var json = JsonSerializer.Serialize(sendLoginInformationEmailRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                "http://innoclinic_notification_api:8080/api/Notification/send-login-information-email", content);
        }
    }
}
