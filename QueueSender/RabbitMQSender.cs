using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace QueueSender
{
    public class RabbitMQSender : IQueueSender
    {
        private readonly RabbitMQSenderConfiguration _rabbitMQSenderConfiguration;

        public RabbitMQSender(IOptions<RabbitMQSenderConfiguration> rabbitMQSenderConfiguration)
        {
            _rabbitMQSenderConfiguration = rabbitMQSenderConfiguration.Value;
        }

        public void SendMessage(string message)
        {         
            var factory = new ConnectionFactory() { HostName = _rabbitMQSenderConfiguration.HostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Todo: get from config
                    channel.QueueDeclare(queue:_rabbitMQSenderConfiguration.QueueName,
                                         durable: false,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange:"",
                                            routingKey: _rabbitMQSenderConfiguration.RoutingKey,
                                            basicProperties: null,
                                            body: body);
                }
            }
        }
    }
}
