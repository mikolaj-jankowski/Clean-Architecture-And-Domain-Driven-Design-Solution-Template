namespace CA.And.DDD.Template.Application.Shared
{
    public static class CacheKeyBuilder
    {
        public static string GetCustomerKey(string email) => $"customer:{email}";
        public static string GetOrderKey(Guid id) => $"order:{id}";
    }
}
