using System.Threading.Tasks;
using Mensageria.Model;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Mensageria.Services
{
    public class MessageService : IMessageService
    {
        private readonly IQueueService _queueService;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public MessageService(ILogger<MessageService> logger, IQueueService queueService, IHostingEnvironment env, IConfiguration configuration)
        {
            _logger = logger;
            _queueService = queueService;
            _env = env;
            _configuration = configuration;
        }

        public async Task SendMessage()
        {
            _logger.LogInformation("Sending message");

            var messageObject = new MessageModel()
            {
                ApplicationName = _env.ApplicationName,
                Message = _configuration.GetValue<string>("MessageText")
            };

            await _queueService.SendAsync(
                @object: messageObject,
                exchangeName: "helloworld",
                routingKey: "message.key");
        }
    }
}