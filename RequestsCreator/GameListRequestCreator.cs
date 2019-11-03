using RequestsCreator;
using System.Collections.Generic;
using System.Net.Http;

namespace GameListProducer
{
    public class GameListRequestCreator : IRequestCreator
    {
        private readonly GameListConfig _gameListConfig;
        private const string key = "key";
        private const string secret = "secret";

        public GameListRequestCreator(GameListConfig gameListConfig )
        {
            _gameListConfig = gameListConfig;
        }

        public HttpRequestMessage GetHttpRequest()
        {
            string keyValue = _gameListConfig.Key;
            string scecretValue = _gameListConfig.Secret;
            var url = _gameListConfig.Url;

            var urlParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(key, keyValue),
                new KeyValuePair<string, string>(secret, scecretValue)
            };

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content = new FormUrlEncodedContent(urlParams);

            return request;
        }
    }
}
