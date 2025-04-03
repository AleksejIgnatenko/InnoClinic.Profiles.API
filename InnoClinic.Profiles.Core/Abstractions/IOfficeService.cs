using InnoClinic.Profiles.Core.Models.OfficeModels;

namespace InnoClinic.Profiles.Application.Services
{
    public interface IOfficeService
    {
        Task UpdateOfficeAsync(OfficeEntity office);
    }
}