using Microsoft.Extensions.Options;
using RequestsCreator;
using System.Net.Http;

namespace RequestsCreator
{
    public class GameListRequestCreator : IRequestCreator
    {
        private readonly GameListRequestConfig _gameListConfig;
        private const string key = "key";
        private const string secret = "secret";

        public GameListRequestCreator(IOptions<GameListRequestConfig> gameListConfig)
        {
            _gameListConfig = gameListConfig.Value;
        }

        public HttpRequestMessage GetHttpRequest()
        {
            var url = $"{_gameListConfig.Url}?{key}={_gameListConfig.Key}&{secret}={_gameListConfig.Secret}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return request;
        }
    }
}
