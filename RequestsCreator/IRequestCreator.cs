using System.Net.Http;

namespace GameListProducer
{
    public interface IRequestCreator
    {
        HttpRequestMessage GetHttpRequest();
    }
}