
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
using System.Text;

namespace GameListReceiver
{
    class RabbitMQReciver
    {
        public static void ReciveMessage(RabbitMQReceiverConfig configuration)
        {
            var factory = new ConnectionFactory() { HostName = configuration.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: configuration.QueueName,
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
                channel.BasicConsume(queue: configuration.QueueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
