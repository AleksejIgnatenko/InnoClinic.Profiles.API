using InnoClinic.Profiles.Core.Models.ReceptionistModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IReceptionistService
    {
        Task CreateReceptionistAsync(string firstName, string lastName, string middleName, string email, string status, Guid officeId);
        Task DeleteReceptionistAsync(Guid id);
        Task<IEnumerable<ReceptionistEntity>> GetAllReceptionistsAsync();
        Task<ReceptionistEntity> GetReceptionistByIdAsync(Guid id);
        Task UpdateReceptionistAsync(Guid id, string firstName, string lastName, string middleName, string status, Guid officeId);
        Task<ReceptionistEntity> GetReceptionistByAccountIdFromTokenAsync(string token);
    }
}