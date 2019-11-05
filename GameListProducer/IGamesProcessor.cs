using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameListProducer
{
    public interface IGamesProcessor
    {
        Task<string> GetGameList();
    }
}