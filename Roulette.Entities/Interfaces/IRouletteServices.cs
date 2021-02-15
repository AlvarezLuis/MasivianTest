using Roulette.Entities.Responses;
using System.Threading.Tasks;

namespace Roulette.Entities.Interfaces
{
    public interface IRouletteServices
    {
        Task<Response> AddRoulette();
        Task<Response> OpenRoulette(int id);
        Task<Response> CloseRoulette(int id);
        Task<Response> GetRoulettes();
        Task<Response> GetRoulette(int id);
    }
}
