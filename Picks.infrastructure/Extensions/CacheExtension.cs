﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WU17.MBA.Picks.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<bool> SetValueAsync<T>(this IDistributedCache cache, string key, T value)
        {
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value));
            return true;
        }

        public static async Task<T> GetValueAsync<T>(this IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
