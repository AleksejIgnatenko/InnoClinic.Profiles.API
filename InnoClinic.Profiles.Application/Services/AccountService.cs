using AutoMapper;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Enums;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;
using Newtonsoft.Json;

namespace InnoClinic.Profiles.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordService _passwordService;
        private readonly IAccountRepository _accountRepository;
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountService(IPasswordService passwordService, IAccountRepository accountRepository, IRabbitMQService rabbitmqService, IMapper mapper, IEmailService emailService)
        {
            _passwordService = passwordService;
            _accountRepository = accountRepository;
            _rabbitmqService = rabbitmqService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Guid> CreateAccountAsync(string email, string fullName, RoleEnum role, string? photoId)
        {
            var password = _passwordService.GeneratePassword();

            var account = new AccountEntity
            {
                Id = Guid.NewGuid(),
                PhotoId = photoId,
                Email = email,
                Password = password,
                Role = role
            };

            await _emailService.SendEmailAsync(account, fullName);

            account.Password = _passwordService.GenerateHash(account.Password);

            await _accountRepository.CreateAsync(account);

            var accountDto = _mapper.Map<AccountDto>(account);

            await _rabbitmqService.PublishMessageAsync(accountDto, RabbitMQQueues.ADD_ACCOUNT_IN_PROFILE_API_QUEUE);

            return account.Id;
        }
    }
}
