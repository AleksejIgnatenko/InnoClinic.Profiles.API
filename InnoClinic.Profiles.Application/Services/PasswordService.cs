using System.Text;

namespace InnoClinic.Profiles.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private static readonly char[] _allowedChars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{};:,.<>/?".ToCharArray();

        public string GeneratePassword()
        {
            int length = 8;

            var random = new Random();
            var passwordBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(_allowedChars.Length);
                passwordBuilder.Append(_allowedChars[index]);
            }

            return passwordBuilder.ToString();
        }

        public string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 10);
        }
    }
}
