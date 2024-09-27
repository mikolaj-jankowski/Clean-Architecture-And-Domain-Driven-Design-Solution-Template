using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Domian.Customers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CA.And.DDD.Template.Application.Customer.ChangeEmail;

public sealed class ChangeEmailCommandHandler : IConsumer<ChangeEmailCommand>
{
    private readonly ICustomerRepository _customerRespository;
    private readonly ILogger<ChangeEmailCommandHandler> _logger;

    public ChangeEmailCommandHandler(ICustomerRepository customerRespository, ILogger<ChangeEmailCommandHandler> logger)
    {
        _customerRespository = customerRespository;
        _logger = logger;
    }


    public async Task Consume(ConsumeContext<ChangeEmailCommand> command)
    {
        var (oldEmail, newEmail) = command.Message;
        var customer = await _customerRespository.GetAsync(oldEmail);

        if (customer == null)
        {
            throw new CustomerNotFoundApplicationException(oldEmail);
        }

        customer.ChangeEmail(new Email(newEmail));

        _logger.LogInformation("Email address for customer '{oldEmail}' changed to '{newEmail}'.", oldEmail, newEmail);

    }

}
