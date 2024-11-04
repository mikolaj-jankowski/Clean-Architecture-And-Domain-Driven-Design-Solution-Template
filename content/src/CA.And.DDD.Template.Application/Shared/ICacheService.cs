namespace CA.And.DDD.Template.Application.Shared
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task ReplaceAsync<T>(string key, T value);
        Task SetAsync<T>(string key, T value, int expirationTimeSeconds = default);
    }
}
