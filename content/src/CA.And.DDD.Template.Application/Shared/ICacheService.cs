namespace CA.And.DDD.Template.Application.Shared
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);
        Task RemoveAsync(string key, CancellationToken cancellationToken);
        Task ReplaceAsync<T>(string key, T value, int expirationTimeSeconds, CancellationToken cancellationToken);
        Task SetAsync<T>(string key, T value, int expirationTimeSeconds = default, CancellationToken cancellationToken = default);
    }
}
