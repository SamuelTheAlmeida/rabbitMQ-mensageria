using System;
using System.Threading;
using System.Threading.Tasks;
using Mensageria.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mensageria.Pages
{
    public class IndexModel : PageModel, IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;
        private Timer timer;
        private ConsumingService _consumingService;
        private int SendMessageInterval => _configuration.GetValue<int>("SendMessageInterval");

        public IndexModel(IServiceProvider services, IConfiguration configuration, ConsumingService consumingService)
        {
            _services = services;
            _configuration = configuration;
            _consumingService = consumingService;
        }

        public void OnGet()
        {
            _consumingService.StartAsync(CancellationToken.None);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(SendMessageInterval));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var messageservice =
                    scope.ServiceProvider
                        .GetRequiredService<IMessageService>();
                messageservice.SendMessage();
            }
        }
    }
}