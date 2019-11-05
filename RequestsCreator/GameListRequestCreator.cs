using RequestsCreator;
using System.Net.Http;

namespace GameListProducer
{
    public class GameListRequestCreator : IRequestCreator
    {
        private readonly GameListRequestConfig _gameListConfig;
        private const string key = "key";
        private const string secret = "secret";

        public GameListRequestCreator(GameListRequestConfig gameListConfig)
        {
            _gameListConfig = gameListConfig;
        }

        public HttpRequestMessage GetHttpRequest()
        {
            var url = $"{_gameListConfig.Url}?{key}={_gameListConfig.Key}&{secret}={_gameListConfig.Secret}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return request;
        }
    }
}
