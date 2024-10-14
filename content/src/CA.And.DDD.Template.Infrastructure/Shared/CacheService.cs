﻿using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CA.And.DDD.Template.Infrastructure.Shared
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<AppSettings> _appSettings;

        //TODO: Add expiration time

        public CacheService(IDistributedCache distributedCache, IOptions<AppSettings> appSettings)
        {
            _distributedCache = distributedCache;
            _appSettings = appSettings;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var cachedValue = await _distributedCache.GetStringAsync(key);
            if(string.IsNullOrEmpty(cachedValue))
            {
                return default;
            }
            var customerDto = JsonConvert.DeserializeObject<T>(cachedValue);
            return customerDto;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_appSettings.Value.Cache.ExpirationTimeSeconds)
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), cacheOptions);
        }

        public async Task RemoveAsync(string key)
            => await _distributedCache.RemoveAsync(key);

        public async Task ReplaceAsync<T>(string key, T value)
        {
            await RemoveAsync(key);
            await SetAsync(key, value);
        }
    }
}