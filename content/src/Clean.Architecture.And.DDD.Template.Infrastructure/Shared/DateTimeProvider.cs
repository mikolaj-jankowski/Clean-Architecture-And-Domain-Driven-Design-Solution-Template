using Clean.Architecture.And.DDD.Template.Domian;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Shared
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private DateTime _date;
        public DateTimeProvider()
        {
            _date = DateTime.UtcNow;
        }

        public DateTime UtcNow => _date;

        public void Set(DateTime dateTime)
        {
            _date = dateTime;
        }
    }
}
