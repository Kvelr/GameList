namespace QueueSender
{
    public class RabbitMQSenderConfiguration
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
    };
}
