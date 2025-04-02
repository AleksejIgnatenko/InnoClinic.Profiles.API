﻿using InnoClinic.Profiles.Infrastructure.RabbitMQ;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace InnoClinic.Profiles.Application.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQSetting _rabbitMqSetting;
        private readonly ConnectionFactory _factory;

        public RabbitMQService(IOptions<RabbitMQSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
        }

        public async Task CreateQueuesAsync()
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                await Task.Run(() =>
                {
                    channel.QueueDeclare(RabbitMQQueues.ADD_DOCTOR_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.ADD_DOCTOR_IN_APPOINTMENTS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.UPDATE_DOCTOR_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.UPDATE_DOCTOR_IN_APPOINTMENTS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.DELETE_DOCTOR_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.DELETE_DOCTOR_IN_APPOINTMENTS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    channel.QueueDeclare(RabbitMQQueues.ADD_RECEPTIONIST_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.UPDATE_RECEPTIONIST_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.DELETE_RECEPTIONIST_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    channel.QueueDeclare(RabbitMQQueues.ADD_PATIENT_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.ADD_PATIENT_IN_APPOINTMENTS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.UPDATE_PATIENT_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.UPDATE_PATIEN_IN_APPOINTMENTST_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.DELETE_PATIENT_IN_AUTHORIZATION_API_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueDeclare(RabbitMQQueues.DELETE_PATIENT_IN_APPOINTMENTS_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
                });
            }
        }

        public async Task PublishMessageAsync(object obj, string queueName)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }
    }
}
