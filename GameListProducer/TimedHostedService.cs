using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using QueueSender;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameListProducer
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IGamesProcessor _gamesProcessor;
        private readonly IConfiguration Configuration;
        private readonly IQueueSender _rabbitMQSender;
        private readonly double _period;   
        private const string periodTimeKey = "GameListProcessorInterval";

        public TimedHostedService(IGamesProcessor gamesProcessor, IConfiguration config, IQueueSender rabbitMQSender)
        {
            _gamesProcessor = gamesProcessor;
            Configuration = config;
            _period = Configuration.GetValue<double>(periodTimeKey);
            _rabbitMQSender = rabbitMQSender;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_period));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var result = await _gamesProcessor.GetGameList();
            _rabbitMQSender.SendMessage(result);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
