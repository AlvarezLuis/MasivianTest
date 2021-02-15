using Microsoft.Extensions.Caching.Distributed;
using Roulette.Adapters.Extensions;
using Roulette.Adapters.Utils;
using Roulette.Entities.Interfaces;
using Roulette.Entities.Models;
using Roulette.Entities.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Adapters
{
    public class BetAdapter : IBetAdapter
    {
        const string BaseKey = "bet_roulette_";
        private readonly IDistributedCache DistributedCache;

        public BetAdapter(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        public async Task<List<BetRequest>> GetBets(string rouletteId)
        {
            DataAccess da = new DataAccess(DistributedCache);
            return await da.GetAllAsync<BetRequest>(BaseKey+ rouletteId);
        }

        public async Task SetBet(Bet bet)
        {
            await DistributedCache.SetCacheValueAsync($"{BaseKey}{bet.RouletteId}_{bet.Id}", bet);
        }
    }
}
