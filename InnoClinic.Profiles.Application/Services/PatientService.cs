using AutoMapper;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.PatientModels;
using InnoClinic.Profiles.DataAccess.Repositories;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;

namespace InnoClinic.Profiles.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IJwtTokenService _jwtTokenService;

        public PatientService(IPatientRepository patientRepository, IAccountRepository accountRepository, IValidationService validationService, IMapper mapper, IRabbitMQService rabbitMQService, IJwtTokenService jwtTokenService)
        {
            _patientRepository = patientRepository;
            _accountRepository = accountRepository;
            _validationService = validationService;
            _mapper = mapper;
            _rabbitMQService = rabbitMQService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task CreatePatientAsync(string firstName, string lastName, string middleName, string dateOfBirth)
        {
            var patient = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                IsLinkedToAccount = false,
                DateOfBirth = dateOfBirth,
            };

            await _patientRepository.CreateAsync(patient);

            var patientDto = _mapper.Map<PatientDto>(patient);

            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.ADD_PATIENT_IN_APPOINTMENTS_QUEUE);
        }

        public async Task CreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber,
            bool isLinkedToAccount, string dateOfBirth, string? photoId, string token)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);
            var account = await _accountRepository.GetByIdAsync(accountId);
            account.PhotoId = photoId;
            account.PhoneNumber = phoneNumber;

            var patient = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                IsLinkedToAccount = isLinkedToAccount,
                DateOfBirth = dateOfBirth,
                Account = account
            };

            var validationAccountErrors = _validationService.Validation(account);
            if ((validationAccountErrors.Count != 0))
            {
                throw new ValidationException(validationAccountErrors);
            }

            var validationPatientErrors = _validationService.Validation(patient);
            if ((validationPatientErrors.Count != 0))
            {
                throw new ValidationException(validationPatientErrors);
            }

            var foundPatients = await _patientRepository.FindMatchingPatientsByCriteriaAsync(patient);

            if (foundPatients.Any())
            {
                throw new DataRepositoryException("Patients found when none were expected", 409, foundPatients);
            }

            await _patientRepository.CreateAsync(patient);

            var patientDto = _mapper.Map<PatientDto>(patient);
            var accountDto = new AccountUpdatePhonePhotoDto(accountId, phoneNumber, photoId);

            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.ADD_PATIENT_IN_APPOINTMENTS_QUEUE);
            await _rabbitMQService.PublishMessageAsync(accountDto, RabbitMQQueues.UPDATE_ACCOUNT_PHONE_PHOTO_QUEUE);

        }

        public async Task ForceCreatePatientAsync(string firstName, string lastName, string middleName, string phoneNumber,
            bool isLinkedToAccount, string dateOfBirth, string? photoId, string token)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);
            var account = await _accountRepository.GetByIdAsync(accountId);
            account.PhoneNumber = phoneNumber;
            account.PhotoId = photoId;

            var patient = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                IsLinkedToAccount = isLinkedToAccount,
                DateOfBirth = dateOfBirth,
                Account = account
            };

            var validationAccountErrors = _validationService.Validation(account);
            if ((validationAccountErrors.Count != 0))
            {
                throw new ValidationException(validationAccountErrors);
            }

            var validationPatientErrors = _validationService.Validation(patient);
            if ((validationPatientErrors.Count != 0))
            {
                throw new ValidationException(validationPatientErrors);
            }

            await _patientRepository.CreateAsync(patient);

            var patientDto = _mapper.Map<PatientDto>(patient);
            var accountDto = new AccountUpdatePhonePhotoDto(accountId, phoneNumber, photoId);

            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.ADD_PATIENT_IN_AUTHORIZATION_API_QUEUE);
            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.ADD_PATIENT_IN_APPOINTMENTS_QUEUE);
            await _rabbitMQService.PublishMessageAsync(accountDto, RabbitMQQueues.UPDATE_ACCOUNT_PHONE_PHOTO_QUEUE);
        }

        public async Task<IEnumerable<PatientEntity>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<PatientEntity> GetPatientByAccountIdFromTokenAsync(string token)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);
            return await _patientRepository.GetByAccountId(accountId);
        }

        public async Task<PatientEntity> GetPatientByIdAsync(Guid patientId)
        {
            return await _patientRepository.GetByIdAsync(patientId);
        }

        public async Task UpdatePatientAsync(Guid id, string firstName, string lastName, string middleName, bool isLinkedToAccount,
            string dateOfBirth, string? photoId)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            patient.FirstName = firstName;
            patient.LastName = lastName;
            patient.MiddleName = middleName;
            patient.IsLinkedToAccount = isLinkedToAccount;
            patient.DateOfBirth = dateOfBirth;
            patient.Account.PhotoId = photoId;

            var validationErrors = _validationService.Validation(patient);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _patientRepository.UpdateAsync(patient);

            var patientDto = _mapper.Map<PatientDto>(patient);

            var accountDto = new AccountUpdatePhonePhotoDto(patient.Account.Id, patient.Account.PhoneNumber, photoId);

            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.UPDATE_PATIEN_IN_APPOINTMENTST_QUEUE);
            await _rabbitMQService.PublishMessageAsync(accountDto, RabbitMQQueues.UPDATE_ACCOUNT_PHONE_PHOTO_QUEUE);
        }

        public async Task AccountConnectionWithThePatient(string token, Guid patientId)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);

            var account = await _accountRepository.GetByIdAsync(accountId);
            var patient = await _patientRepository.GetByIdAsync(patientId);

            patient.Account = account;
            patient.IsLinkedToAccount = true;

            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            await _patientRepository.DeleteAsync(patient);

            var patientDto = _mapper.Map<PatientDto>(patient);
            await _rabbitMQService.PublishMessageAsync(patientDto, RabbitMQQueues.DELETE_PATIENT_IN_APPOINTMENTS_QUEUE);
        }
    }
}
