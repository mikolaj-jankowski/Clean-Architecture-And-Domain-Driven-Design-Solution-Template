using MassTransit;
using System.Diagnostics;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class CustomerCreatedIntegrationEventHandler : IConsumer<CustomerCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedIntegrationEvent> context)
        {
            //The CustomerCreatedIntegrationEvent was produced by the Customer aggregate,
            //thus this handler should have never been placed here. However this repo is meant
            //to provide a full working template, so it was placed here just to demostrate
            //how to register and handle incoming itegration events.

            return Task.CompletedTask;
        }
    }
}
