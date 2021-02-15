using Microsoft.Extensions.Caching.Distributed;
using Roulette.Adapters.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Adapters.Utils
{
    public class DataAccess 
    {
        private readonly IDistributedCache DistributedCache;

        public DataAccess(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        public async Task<List<T>> GetAllAsync<T>(string contain) where T : class
        {
            var result = new List<T>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{Environment.GetEnvironmentVariable("redisserver")}:{Environment.GetEnvironmentVariable("redisserverport")},allowAdmin=true"))
            {
                var keys = redis.GetServer(Environment.GetEnvironmentVariable("redisserver"), int.Parse(Environment.GetEnvironmentVariable("redisserverport"))).Keys();

                var keysArr = keys.Where(key => ((string)key).StartsWith(contain)).ToList();

                foreach (string key in keysArr)
                {
                    var roulettte = await DistributedCache.GetCacheValueAsync<T>(key);
                    result.Add(roulettte);
                }
            }
            return result;
        }

    }
}
