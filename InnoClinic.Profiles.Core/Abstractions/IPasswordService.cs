namespace InnoClinic.Profiles.Application.Services
{
    public interface IPasswordService
    {
        string GenerateHash(string password);
        string GeneratePassword();
    }
}