using Microsoft.Extensions.Hosting;
using QueueReceiver;
using System.Threading;
using System.Threading.Tasks;

namespace GameListConsumer
{
    public class HostedService : IHostedService
    {
        private IQueueReceiver _rabbitMQReceiver;

        public HostedService(IQueueReceiver rabbitMQReceiver)
        {
            _rabbitMQReceiver = rabbitMQReceiver;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _rabbitMQReceiver.ReciveMessage());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
