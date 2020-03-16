using System;
using System.Threading.Tasks;
using Mensageria.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection;

namespace Mensageria.Handler
{
    public class MessageHandler : IAsyncMessageHandler
    {
        private readonly ILogger logger;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly IHostingEnvironment env;

        public MessageHandler(ILogger<MessageHandler> logger, IHubContext<MessageHub> hubContext, IHostingEnvironment env)
        {
            this.logger = logger;
            this.hubContext = hubContext;
            this.env = env;
        }

        public async Task Handle(string message, string routingKey)
        {
            this.logger.LogInformation(message);
            await this.hubContext.Clients.All.SendAsync("ReceiveMessage", env.ApplicationName, message);
        }
    }
}