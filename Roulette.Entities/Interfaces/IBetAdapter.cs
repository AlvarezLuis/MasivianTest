using Roulette.Entities.Models;
using Roulette.Entities.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Entities.Interfaces
{
    public interface IBetAdapter
    {
        Task SetBet(Bet bet);
        Task<List<BetRequest>> GetBets(string rouletteId);
    }
}
