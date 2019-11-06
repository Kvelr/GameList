using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace QueueReceiver
{
    public class RabbitMQReceiver
    {
        private RabbitMQReceiverConfig _rabbitMQReceiverConfig;

        public RabbitMQReceiver(IOptions<RabbitMQReceiverConfig> rabbitMQReceiverConfig)
        {
            var _rabbitMQReceiverConfig = rabbitMQReceiverConfig.Value;
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
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
