using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class UnitOfWorkFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWorkFilter(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);
            await _appDbContext.SaveChangesAsync();
        }

        public void Probe(ProbeContext context) { }
    }
}
