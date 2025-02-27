using Microsoft.AspNetCore.DataProtection;
using MailKit.Net.Smtp;
using MimeKit;
using InnoClinic.Profiles.Core.Models.AccountModels;


namespace InnoClinic.Profiles.Application.Services
{
    public class EmailService : IEmailService
    {
        private const string _emai = "innoclinic33@gmail.com";
        private const string _emaiAppPassword = "spaz tebr scpd lahu";

        public async Task SendEmailAsync(AccountEntity account, string fullName)
        {
            //create email message
            var subject = "Данные для входа";
            string message = $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        h1 {{
                            color: #333333;
                        }}
                        p {{
                            color: #555555;
                            line-height: 1.6;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 10px 20px;
                            margin-top: 20px;
                            background-color: #007BFF;
                            color: #ffffff !important;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold; 
                        }}
                        .button:hover {{
                            background-color: #0056b3; 
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #999999;
                            font-size: 12px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Добро пожаловать в InnoClinic, {fullName}!</h1>
                        <p>Здравствуйте!</p>
                        <p>Мы рады сообщить вам, что вы успешно зарегистрированы в системе InnoClinic. Теперь вы можете управлять своей практикой и взаимодействовать с пациентами.</p>
                        <p>Ваши учетные данные для входа:</p>
                        <p><strong>Логин:</strong> {account.Email}</p>
                        <p><strong>Пароль:</strong> {account.Password}</p>
                        <p>Для доступа к вашей учетной записи, пожалуйста, перейдите по следующей ссылке:</p>
                        <p><a href=""http://localhost:4001"" class='button'>Войти в систему</a></p>
                        <div class='footer'>
                            С уважением,<br>
                            Администрация сайта InnoClinic
                        </div>
                    </div>
                </body>
            </html>";

            //send email
            await SendAsync(account.Email, subject, message);
        }

        private async Task SendAsync(string email, string subject, string message)
        {
            //create, configuration and send email message
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Inno Clinic", _emai));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emai, _emaiAppPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
