using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace QueueReceiver
{
    public class RabbitMQReceiver : IQueueReceiver
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
    }
}
