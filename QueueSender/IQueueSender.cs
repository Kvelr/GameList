namespace QueueSender
{
    public interface IQueueSender
    {
         void SendMessage(string message);
    }
}