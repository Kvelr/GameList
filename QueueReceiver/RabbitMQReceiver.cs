using GameListDal;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace QueueReceiver
{
    public class RabbitMQReceiver : IQueueReceiver
    {
        private readonly RabbitMQReceiverConfig _rabbitMQReceiverConfig;
        private readonly GamesDal _games;

        public RabbitMQReceiver(IOptions<RabbitMQReceiverConfig> rabbitMQReceiverConfig, GamesDal gamesDal)
        {
            _rabbitMQReceiverConfig = rabbitMQReceiverConfig.Value;
            _games = gamesDal;
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

                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(_rabbitMQReceiverConfig.QueueName, true, consumer);            

                while (true)
                {
                    var ea = consumer.Queue.Dequeue();
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);                  
                }                
            }
        }      
    }
}
