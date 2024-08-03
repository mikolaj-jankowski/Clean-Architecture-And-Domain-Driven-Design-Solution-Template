using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Customers;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;

public class CreateCustomerCommandHandler : IConsumer<CreateCustomerCommand>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCustomerCommandHandler(ICustomerRespository customerRespository, ILogger<CreateCustomerCommandHandler> logger,
        IDateTimeProvider dateTimeProvider)
    {
        _customerRespository = customerRespository;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }



    public async Task Consume(ConsumeContext<CreateCustomerCommand> command)
    {
        _logger.LogInformation($"Inserting a customer: {command.Message.Name} {command.Message.Surname}");

        var customer = Domian.Customers.Customer.CreateCustomer(command.Message.Name, command.Message.Surname, command.Message.BirthDate.AddYears(-30), _dateTimeProvider);
        await _customerRespository.AddAsync(customer);
        await command.RespondAsync<CreateCustomerResponse>(new CreateCustomerResponse(customer.CustomerId.Value));
    }


    public class CustomerCreatedIntegrationEventHandler : IConsumer<CustomerCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedIntegrationEvent> context)
        {
            Debug.WriteLine($"{DateTime.UtcNow}. Got here.");
            Debug.WriteLine(context.Message.CustomerId);
            return Task.CompletedTask;
        }
    }

}
