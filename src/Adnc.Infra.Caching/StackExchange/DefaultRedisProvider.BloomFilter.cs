﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.StackExchange
{
    /// <summary>
    /// Default redis caching provider.
    /// </summary>
    public partial class DefaultRedisProvider : Adnc.Infra.Caching.IRedisProvider
    {
        public async Task<bool> BfAddAsync(string key, string value)
        {
            return await _redisDb.BfAddAsync(key, value);
        }

        public async Task<bool[]> BfAddAsync(string key, IEnumerable<string> values)
        {
            var redisValues = values.Select(x => (RedisValue)x);
            return await _redisDb.BfAddAsync(key, redisValues);
        }

        public async Task<bool> BfExistsAsync(string key, string value)
        {
            return await _redisDb.BfExistsAsync(key, value);
        }

        public async Task<bool[]> BfExistsAsync(string key, IEnumerable<string> values)
        {
            var redisValues = values.Select(x => (RedisValue)x);
            return await _redisDb.BfExistsAsync(key, redisValues);
        }

        public async Task BfReserveAsync(string key, double errorRate, int initialCapacity)
        {
            await _redisDb.BfReserveAsync(key, errorRate, initialCapacity);
        }
    }
}
