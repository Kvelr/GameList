using RequestsCreator;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameListProducer
{
    public class GameListProcessor : IGamesProcessor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IRequestCreator _requestCreator;

        public GameListProcessor(IHttpClientFactory clientFactory, IRequestCreator requestCreator)
        {
            _clientFactory = clientFactory;
            _requestCreator = requestCreator;
        }

        public async Task<string> GetGameList()
        {
            string gameList = null;
            var request = _requestCreator.GetHttpRequest();
            var client = _clientFactory.CreateClient();

            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var straemReader = new StreamReader(responseStream, Encoding.UTF8);
                gameList = await straemReader.ReadToEndAsync();
            }

            return gameList;
        }

    }
}
