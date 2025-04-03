using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.OfficeModels;
using InnoClinic.Profiles.DataAccess.Repositories;

namespace InnoClinic.Profiles.Application.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IReceptionistRepository _receptionistRepository;

        public OfficeService(IOfficeRepository officeRepository, IDoctorRepository doctorRepository, IReceptionistRepository receptionistRepository)
        {
            _officeRepository = officeRepository;
            _doctorRepository = doctorRepository;
            _receptionistRepository = receptionistRepository;
        }

        public async Task UpdateOfficeAsync(OfficeEntity office)
        {
            if (!office.IsActive)
            {
                await _doctorRepository.UpdateAsync(office.Id, "Inactive");
                await _receptionistRepository.UpdateAsync(office.Id, "Inactive");
            }

            await _officeRepository.UpdateAsync(office);
        }
    }
}
