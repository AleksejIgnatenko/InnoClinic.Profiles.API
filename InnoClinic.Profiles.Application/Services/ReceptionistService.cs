using AutoMapper;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Exceptions;
using InnoClinic.Profiles.Core.Models.ReceptionistModels;
using InnoClinic.Profiles.DataAccess.Repositories;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;

namespace InnoClinic.Profiles.Application.Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IValidationService _validationService;
        private readonly IAccountService _accountService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly IRabbitMQService _rabbitMQService;

        public ReceptionistService(IReceptionistRepository receptionistRepository, IOfficeRepository officeRepository, IAccountRepository accountRepository, IValidationService validationService, IAccountService accountService, IJwtTokenService jwtTokenService, IMapper mapper, IRabbitMQService rabbitMQService)
        {
            _receptionistRepository = receptionistRepository;
            _officeRepository = officeRepository;
            _accountRepository = accountRepository;
            _validationService = validationService;
            _accountService = accountService;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _rabbitMQService = rabbitMQService;
        }

        public async Task CreateReceptionistAsync(string firstName, string lastName, string middleName, string email, string status, Guid officeId)
        {
            var accountId = await _accountService.CreateAccountAsync(email, firstName + " " + lastName, Core.Enums.RoleEnum.Receptionist);

            var office = await _officeRepository.GetByIdAsync(officeId);
            var account = await _accountRepository.GetByIdAsync(accountId);

            var receptionist = new ReceptionistEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Account = account,
                Status = status,
                Office = office
            };

            var validationErrors = _validationService.Validation(receptionist);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _receptionistRepository.CreateAsync(receptionist);

            var receptionistDto = _mapper.Map<ReceptionistDto>(receptionist);
            await _rabbitMQService.PublishMessageAsync(receptionistDto, RabbitMQQueues.ADD_RECEPTIONIST_QUEUE);
        }

        public async Task<IEnumerable<ReceptionistEntity>> GetAllReceptionistsAsync()
        {
            return await _receptionistRepository.GetAllAsync();
        }

        public async Task<ReceptionistEntity> GetReceptionistByIdAsync(Guid id)
        {
            return await _receptionistRepository.GetByIdAsync(id);
        }

        public async Task<ReceptionistEntity> GetReceptionistByAccountIdFromTokenAsync(string token)
        {
            var accountId = _jwtTokenService.GetAccountIdFromAccessToken(token);
            return await _receptionistRepository.GetByAccountId(accountId);
        }

        public async Task UpdateReceptionistAsync(Guid id, string firstName, string lastName, string middleName, string status, Guid officeId)
        {
            var office = await _officeRepository.GetByIdAsync(officeId);

            var receptionist = new ReceptionistEntity
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Status = status,
                Office = office
            };

            var validationErrors = _validationService.Validation(receptionist);

            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _receptionistRepository.UpdateAsync(receptionist);

            var receptionistDto = _mapper.Map<ReceptionistDto>(receptionist);
            await _rabbitMQService.PublishMessageAsync(receptionistDto, RabbitMQQueues.UPDATE_RECEPTIONIST_QUEUE);
        }

        public async Task DeleteReceptionistAsync(Guid id)
        {
            var receptionist = await _receptionistRepository.GetByIdAsync(id);
            await _receptionistRepository.DeleteAsync(receptionist);

            var receptionistDto = _mapper.Map<ReceptionistDto>(receptionist);
            await _rabbitMQService.PublishMessageAsync(receptionistDto, RabbitMQQueues.DELETE_RECEPTIONIST_QUEUE);
        }
    }
}
