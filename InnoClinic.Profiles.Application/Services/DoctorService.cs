using AutoMapper;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.DoctorModels;
using InnoClinic.Profiles.DataAccess.Repositories;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;

namespace InnoClinic.Profiles.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IValidationService _validationService;
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IJwtTokenService _jwtTokenService;

        public DoctorService(IDoctorRepository doctorRepository, IOfficeRepository officeRepository, IAccountRepository accountRepository, ISpecializationRepository specializationRepository, IValidationService validationService, IRabbitMQService rabbitmqService, IMapper mapper, IAccountService accountService, IJwtTokenService jwtTokenService)
        {
            _doctorRepository = doctorRepository;
            _officeRepository = officeRepository;
            _accountRepository = accountRepository;
            _specializationRepository = specializationRepository;
            _validationService = validationService;
            _rabbitmqService = rabbitmqService;
            _mapper = mapper;
            _accountService = accountService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task CreateDoctorAsync(string firstName, string lastName, string middleName, int cabinetNumber,
            string dateOfBirth, string email, Guid specializationId, Guid officeId, string careerStartYear, string status, string? photoId)
        {
            var accountId = await _accountService.CreateAccountAsync(email, firstName + " " + lastName, Core.Enums.RoleEnum.Doctor, photoId);

            var specialization = await _specializationRepository.GetByIdAsync(specializationId);
            var office = await _officeRepository.GetByIdAsync(officeId);
            var account = await _accountRepository.GetByIdAsync(accountId);

            var doctor = new DoctorEntity
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
                Status = status,
            };

            var validationErrors = _validationService.Validation(doctor);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _doctorRepository.CreateAsync(doctor);

            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            var accountDto = new AccountUpdatePhonePhotoDto(doctor.Account.Id, doctor.Account.PhoneNumber, photoId);

            await _rabbitmqService.PublishMessageAsync(doctorDto, RabbitMQQueues.ADD_DOCTOR_QUEUE);
            await _rabbitmqService.PublishMessageAsync(accountDto, RabbitMQQueues.UPDATE_ACCOUNT_PHONE_PHOTO_QUEUE);
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<DoctorEntity> GetDoctorByIdAsync(Guid id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllDoctorsAtWorkAsync()
        {
            return await _doctorRepository.GetAllAtWorkAsync();
        }

        public async Task<DoctorEntity> GetDoctorByAccountIdFromTokenAsync(string token)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);
            return await _doctorRepository.GetByAccountId(accountId);
        }

        public async Task<DoctorEntity> GetDoctorByAccountIdAsync(Guid accountId)
        {
            return await _doctorRepository.GetByAccountId(accountId);
        }

        public async Task UpdateDoctorAsync(Guid id, string firstName, string lastName, string middleName, int cabinetNumber,
            string dateOfBirth, Guid specializationId, Guid officeId, string careerStartYear, string status, string? photoId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            var specialization = await _specializationRepository.GetByIdAsync(specializationId);
            var office = await _officeRepository.GetByIdAsync(officeId);

            doctor.FirstName = firstName;
            doctor.LastName = lastName;
            doctor.MiddleName = middleName;
            doctor.CabinetNumber = cabinetNumber;
            doctor.DateOfBirth = dateOfBirth;
            doctor.Specialization = specialization;
            doctor.Office = office;
            doctor.CareerStartYear = careerStartYear;
            doctor.Status = status;
            doctor.Account.PhotoId = photoId;

            var validationErrors = _validationService.Validation(doctor);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _doctorRepository.UpdateAsync(doctor);

            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            var accountDto = new AccountUpdatePhonePhotoDto(doctor.Account.Id, doctor.Account.PhoneNumber, photoId);

            await _rabbitmqService.PublishMessageAsync(doctorDto, RabbitMQQueues.UPDATE_DOCTOR_QUEUE);
            await _rabbitmqService.PublishMessageAsync(accountDto, RabbitMQQueues.UPDATE_ACCOUNT_PHONE_PHOTO_QUEUE);
        }

        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            await _doctorRepository.DeleteAsync(doctor);

            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            await _rabbitmqService.PublishMessageAsync(doctorDto, RabbitMQQueues.DELETE_DOCTOR_QUEUE);
        }
    }
}
