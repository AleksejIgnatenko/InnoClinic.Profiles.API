using System.Text;
using AutoMapper;
using InnoClinic.Profiles.Core.Abstractions;
using InnoClinic.Profiles.Core.Dto;
using InnoClinic.Profiles.Core.Models;
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
        private readonly ISpecializationRepository _specializationRepository;

        public RabbitMQListener(IOptions<RabbitMQSetting> rabbitMqSetting, IMapper mapper, IOfficeRepository officeRepository, IAccountRepository accountRepository, ISpecializationRepository specializationRepository)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            var factory = new ConnectionFactory { HostName = _rabbitMqSetting.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: RabbitMQQueues.ADD_ACCOUNT_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: RabbitMQQueues.ADD_OFFICE_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _mapper = mapper;
            _officeRepository = officeRepository;
            _accountRepository = accountRepository;
            _specializationRepository = specializationRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var addAccountConsumer = new EventingBasicConsumer(_channel);
            addAccountConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var accountDto = JsonConvert.DeserializeObject<AccountDto>(content);
                var account = _mapper.Map<AccountModel>(accountDto);

                await _accountRepository.CreateAsync(account);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_ACCOUNT_QUEUE, false, addAccountConsumer);

            var addOfficeConsumer = new EventingBasicConsumer(_channel);
            addOfficeConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var officeDto = JsonConvert.DeserializeObject<OfficeDto>(content);
                var office = _mapper.Map<OfficeModel>(officeDto);

                await _officeRepository.CreateAsync(office);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_OFFICE_QUEUE, false, addOfficeConsumer);

            var addSpecializationConsumer = new EventingBasicConsumer(_channel);
            addSpecializationConsumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(content);

                var specializationDto = JsonConvert.DeserializeObject<SpecializationDto>(content);
                var specialization = _mapper.Map<SpecializationModel>(specializationDto);

                await _specializationRepository.CreateAsync(specialization);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMQQueues.ADD_SPECIALIZATION_QUEUE, false, addSpecializationConsumer);

            return Task.CompletedTask;
        }
    }
}
