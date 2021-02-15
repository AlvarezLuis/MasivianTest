using Roulette.Entities.Request;
using Roulette.Entities.Responses;
using System.Threading.Tasks;

namespace Roulette.Entities.Interfaces
{
    public interface IBetServices
    {
        Task<Response> AddBet(BetRequest bet);
        Task<Response> GetAllForRoulette(Models.Roulette roulette);
    }
}
