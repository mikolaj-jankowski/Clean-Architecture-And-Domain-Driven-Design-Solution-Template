namespace CA.And.DDD.Template.Domain
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
        public void Set(DateTime dateTime);
    }
}
