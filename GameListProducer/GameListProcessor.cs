using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameListProducer
{
    public class GameListProcessor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IRequestCreator _requestCreator;

        public GameListProcessor(IHttpClientFactory clientFactory, IRequestCreator requestCreator)
        {
            _clientFactory = clientFactory;
            _requestCreator = requestCreator;
        }

        public async Task<IEnumerable<string>> GetGameList()
        {
            var request = _requestCreator.GetHttpRequest();
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var gameList = responseStream.ToString();
               
            }

            throw new NotImplementedException();
        }

    }
}
