using Clean.Architecture.And.DDD.Template.Domian.Customers;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;

public sealed class CreateCustomerCommandHandler : IConsumer<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRespository;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    public CreateCustomerCommandHandler(ICustomerRepository customerRespository, ILogger<CreateCustomerCommandHandler> logger)
    {
        _customerRespository = customerRespository;
        _logger = logger;
    }



    public async Task Consume(ConsumeContext<CreateCustomerCommand> command)
    {
        var customer = Domian.Customers.Customer.CreateCustomer(
            new CustomerId(Guid.NewGuid()),
            new FullName(command.Message.FullName),
            new Age(command.Message.BirthDate),
            new Email(command.Message.Email),
            new Address(command.Message.Street, command.Message.HouseNumber, command.Message.FlatNumber, command.Message.Country, command.Message.PostalCode));

        await _customerRespository.AddAsync(customer);
        await command.RespondAsync<CreateCustomerResponse>(new CreateCustomerResponse(customer.CustomerId.Value));

        _logger.LogInformation("Created a customer: {FullName}, {Email}", command.Message.FullName, command.Message.Email);
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
