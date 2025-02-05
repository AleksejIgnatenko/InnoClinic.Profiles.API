
namespace InnoClinic.Profiles.Application.Services
{
    public interface IRabbitMQService
    {
        Task CreateQueuesAsync();
        Task PublishMessageAsync(object obj, string queueName);
    }
}