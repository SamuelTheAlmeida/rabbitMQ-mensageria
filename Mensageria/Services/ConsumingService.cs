using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Core.DependencyInjection;

namespace Mensageria.Services
{
    public class ConsumingService : IHostedService
    {
        readonly IQueueService queueService;
        private readonly ILogger logger;

        public ConsumingService(IQueueService queueService,
            ILogger<ConsumingService> logger)
        {
            this.logger = logger;
            this.queueService = queueService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Starting consuming.");
            this.queueService.StartConsuming();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Stopping consuming.");
            return Task.CompletedTask;
        }
    }
}