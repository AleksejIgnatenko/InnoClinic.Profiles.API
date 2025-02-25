
namespace InnoClinic.Profiles.Application.Services
{
    public interface IJwtTokenService
    {
        Guid GetAccountIdFromAccessToken(string jwtToken);
    }
}