using Clean.Architecture.And.DDD.Template.Domian.Employees.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Employee.CreateEmployee.EventHandlers
{
    public class EmployeeCreatedDomainEventHandler : IConsumer<EmployeeCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<EmployeeCreatedDomainEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
