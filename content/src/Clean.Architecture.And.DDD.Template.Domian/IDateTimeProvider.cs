namespace Clean.Architecture.And.DDD.Template.Domian
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
        public void Set(DateTime dateTime);
    }
}
