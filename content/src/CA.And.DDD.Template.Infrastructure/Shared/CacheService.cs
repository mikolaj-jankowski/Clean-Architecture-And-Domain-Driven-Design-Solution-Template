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
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            var cachedValue = await _distributedCache.GetStringAsync(key);
            if(string.IsNullOrEmpty(cachedValue))
            {
                return default(T?);
            }
            var customerDto = JsonConvert.DeserializeObject<T>(cachedValue);
            return customerDto;
        }

        public async Task SetAsync<T>(string key, T value, int expirationTimeSeconds = default, CancellationToken cancellationToken = default)
        {
            if(expirationTimeSeconds == default)
            {
                expirationTimeSeconds = _appSettings.Value.Cache.ExpirationTimeSeconds;
            }
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTimeSeconds)
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), cacheOptions);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
            => await _distributedCache.RemoveAsync(key);

        public async Task ReplaceAsync<T>(string key, T value, int expirationTimeSeconds, CancellationToken cancellationToken)
        {
            await RemoveAsync(key, cancellationToken);
            await SetAsync(key, value, expirationTimeSeconds, cancellationToken);
        }
    }
}
