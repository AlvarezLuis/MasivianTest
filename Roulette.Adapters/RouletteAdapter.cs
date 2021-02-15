using Microsoft.Extensions.Caching.Distributed;
using Roulette.Entities.Interfaces;
using Roulette.Adapters.Extensions;
using System.Threading.Tasks;
using model = Roulette.Entities.Models;
using System.Collections.Generic;
using Roulette.Adapters.Utils;

namespace Roulette.Adapters
{
    public class RouletteAdapter : IRouletteAdapter
    {
        const string BaseKey = "roullete_";
        private readonly IDistributedCache DistributedCache;

        public RouletteAdapter(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }
        public async Task<model.Roulette> GetRoulette(int Id)
        {
            return await DistributedCache.GetCacheValueAsync<model.Roulette>(BaseKey+Id.ToString());
        }

        public async Task<List<model.Roulette>> GetRoulettes()
        {
            DataAccess da = new DataAccess(DistributedCache);
            return await da.GetAllAsync<model.Roulette>(BaseKey);            
        }

        public async Task SetRoulette(model.Roulette roulette)
        {
            await DistributedCache.SetCacheValueAsync(BaseKey + roulette.Id.ToString(), roulette);
        }
    }
}
