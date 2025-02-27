using System.Text;
using AutoMapper;
using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Models.AccountModels;
using InnoClinic.Profiles.Core.Models.OfficeModels;
using InnoClinic.Profiles.Core.Models.SpecializationModels;
using InnoClinic.Profiles.DataAccess.Repositories;
using InnoClinic.Profiles.Infrastructure.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InnoClinic.Profiles.Application.RabbitMQ
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly RabbitMQSetting _rabbitMqSetting;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IOfficeService _officeService;
        private readonly ISpecializationRepository _specializationRepository;

        public RabbitMQListener(IOptions<RabbitMQSetting> rabbitMqSetting, IMapper mapper, IAccountRepository accountRepository, ISpecializationRepository specializationRepository, IOfficeService officeService, IOfficeRepository officeRepository)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _mapper = mapper;
            _accountRepository = accountRepository;
            _specializationRepository = specializationRepository;
            _officeService = officeService;
            _officeRepository = officeRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            #region account
            var addAccountConsumer = new EventingBasicConsumer(_channel);
            addAccountConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var accountDto = JsonConvert.DeserializeObject<AccountDto>(content);
                var account = _mapper.Map<AccountEntity>(accountDto);

                await _accountRepository.CreateAsync(account);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_ACCOUNT_QUEUE, false, addAccountConsumer);

            var updateAccountConsumer = new EventingBasicConsumer(_channel);
            updateAccountConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var accountDto = JsonConvert.DeserializeObject<AccountDto>(content);
                var account = _mapper.Map<AccountEntity>(accountDto);

                await _accountRepository.UpdateAsync(account);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.UPDATE_ACCOUNT_QUEUE, false, updateAccountConsumer);

            var deleteAccountConsumer = new EventingBasicConsumer(_channel);
            deleteAccountConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var accountDto = JsonConvert.DeserializeObject<AccountDto>(content);
                var account = _mapper.Map<AccountEntity>(accountDto);

                await _accountRepository.DeleteAsync(account);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.DELETE_ACCOUNT_QUEUE, false, deleteAccountConsumer);
            #endregion

            #region office
            var addOfficeConsumer = new EventingBasicConsumer(_channel);
            addOfficeConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var officeDto = JsonConvert.DeserializeObject<OfficeDto>(content);
                var office = _mapper.Map<OfficeEntity>(officeDto);

                await _officeRepository.CreateAsync(office);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_OFFICE_QUEUE, false, addOfficeConsumer);

            var updateOfficeConsumer = new EventingBasicConsumer(_channel);
            updateOfficeConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var officeDto = JsonConvert.DeserializeObject<OfficeDto>(content);
                var office = _mapper.Map<OfficeEntity>(officeDto);

                await _officeService.UpdateOfficeAsync(office);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.UPDATE_OFFICE_QUEUE, false, updateOfficeConsumer);

            var deleteOfficeConsumer = new EventingBasicConsumer(_channel);
            deleteOfficeConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var officeDto = JsonConvert.DeserializeObject<OfficeDto>(content);
                var office = _mapper.Map<OfficeEntity>(officeDto);

                await _officeRepository.DeleteAsync(office);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.DELETE_OFFICE_QUEUE, false, deleteOfficeConsumer);
            #endregion

            #region specialization
            var addSpecializationConsumer = new EventingBasicConsumer(_channel);
            addSpecializationConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(content);

                var specializationDto = JsonConvert.DeserializeObject<SpecializationDto>(content);
                var specialization = _mapper.Map<SpecializationEntity>(specializationDto);

                await _specializationRepository.CreateAsync(specialization);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_SPECIALIZATION_QUEUE, false, addSpecializationConsumer);

            var updateSpecializationConsumer = new EventingBasicConsumer(_channel);
            updateSpecializationConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(content);

                var specializationDto = JsonConvert.DeserializeObject<SpecializationDto>(content);
                var specialization = _mapper.Map<SpecializationEntity>(specializationDto);

                await _specializationRepository.UpdateAsync(specialization);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.UPDATE_SPECIALIZATION_QUEUE, false, updateSpecializationConsumer);

            var deleteSpecializationConsumer = new EventingBasicConsumer(_channel);
            deleteSpecializationConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(content);

                var specializationDto = JsonConvert.DeserializeObject<SpecializationDto>(content);
                var specialization = _mapper.Map<SpecializationEntity>(specializationDto);

                await _specializationRepository.DeleteAsync(specialization);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.DELETE_SPECIALIZATION_QUEUE, false, deleteSpecializationConsumer);
            #endregion

            return Task.CompletedTask;
        }
    }
}
