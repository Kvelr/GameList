using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueReceiver
{
    public class RabbitMQReceiver : IQueueReceiver, IHostedService
    {
        private readonly RabbitMQReceiverConfig _rabbitMQReceiverConfig;

        public RabbitMQReceiver(IOptions<RabbitMQReceiverConfig> rabbitMQReceiverConfig)
        {
            _rabbitMQReceiverConfig = rabbitMQReceiverConfig.Value;
            ReciveMessage();
        }

        public void ReciveMessage()
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQReceiverConfig.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQReceiverConfig.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: _rabbitMQReceiverConfig.QueueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => ReciveMessage());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
