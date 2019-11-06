using System.Net.Http;

namespace RequestsCreator
{
    public interface IRequestCreator
    {
        HttpRequestMessage GetHttpRequest();
    }
}