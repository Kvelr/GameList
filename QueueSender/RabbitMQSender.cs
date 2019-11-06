using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace QueueSender
{
    public class RabbitMQSender
    {
        private readonly string hostName;
        private readonly string queueName;
        private readonly string routingKey;

        public RabbitMQSender()
        {           
        }

        public void SendMessage(string message)
        {         
            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Todo: get from config
                    channel.QueueDeclare(queueName);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", routingKey, null, body);
                }
            }
        }
    }
}
