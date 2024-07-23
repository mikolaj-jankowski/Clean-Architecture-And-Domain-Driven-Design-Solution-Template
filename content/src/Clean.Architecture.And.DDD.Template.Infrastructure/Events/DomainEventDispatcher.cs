using Clean.Architecture.And.DDD.Template.Domian;
using MassTransit.Mediator;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Dispatch(IDomainEvent domainEvent)
        {
            _mediator.Publish(domainEvent);
            return Task.CompletedTask;

        }
    }
}
