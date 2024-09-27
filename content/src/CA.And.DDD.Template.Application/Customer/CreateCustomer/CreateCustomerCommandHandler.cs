using CA.And.DDD.Template.Domian.Customers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer;

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
        var (fullName, birthDate, email, street, houseNumber, flatNumber, country, postalCode) = command.Message;

        var customer = Domian.Customers.Customer.CreateCustomer(
            new CustomerId(Guid.NewGuid()),
            new FullName(fullName),
            new Age(birthDate),
            new Email(email),
            new Address(street, houseNumber, flatNumber, country, postalCode));

        await _customerRespository.AddAsync(customer);
        await command.RespondAsync<CreateCustomerResponse>(new CreateCustomerResponse(customer.CustomerId.Value, customer.Email.Value));

        _logger.LogInformation("Created a customer: {FullName}, {Email}", command.Message.FullName, command.Message.Email);
    }

}

