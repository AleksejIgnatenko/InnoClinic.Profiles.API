using System.Data.Entity;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models;
using InnoClinic.Profiles.DataAccess.Repositories;

namespace InnoClinic.Profiles.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public DoctorService(IDoctorRepository doctorRepository, IOfficeRepository officeRepository, IAccountRepository accountRepository, ISpecializationRepository specializationRepository)
        {
            _doctorRepository = doctorRepository;
            _officeRepository = officeRepository;
            _accountRepository = accountRepository;
            _specializationRepository = specializationRepository;
        }

        public async Task CreateDoctorAsync(string firstName, string lastName, string middleName, int cabinetNumber,
            DateTime dateOfBirth, Guid accountId, Guid specializationId, Guid officeId, DateTime careerStartYear, string status)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            var specialization = await _specializationRepository.GetByIdAsync(specializationId);
            var office = await _officeRepository.GetByIdAsync(officeId);

            var doctor = new DoctorModel
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                CabinetNumber = cabinetNumber,
                DateOfBirth = dateOfBirth,
                Account = account,
                Specialization = specialization,
                Office = office,
                CareerStartYear = careerStartYear,
                Status = status
            };

            await _doctorRepository.CreateAsync(doctor);
        }

        public async Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<IEnumerable<DoctorModel>> GetAllDoctorsAtWorkAsync()
        {
            return await _doctorRepository.GetAllAtWorkAsync();
        }
    }
}
