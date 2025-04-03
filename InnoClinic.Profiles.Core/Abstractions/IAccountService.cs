using InnoClinic.Profiles.Core.Enums;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IAccountService
    {
        Task<Guid> CreateAccountAsync(string email, string fullName, RoleEnum role, string? photoId);
    }
}