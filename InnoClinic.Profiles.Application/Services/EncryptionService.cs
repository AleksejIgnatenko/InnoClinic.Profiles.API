using System.Security.Cryptography;
using System.Text;

namespace InnoClinic.Profiles.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string EncryptData(string data)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(File.ReadAllText("publicKey.xml"));

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedBytes = rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}