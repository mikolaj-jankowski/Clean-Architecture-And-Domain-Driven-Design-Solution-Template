using Clean.Architecture.And.DDD.Template.Application.Exceptions;
using Clean.Architecture.And.DDD.Template.Domian.Customers;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Consume(ConsumeContext<GetCustomerQuery> query)
        {
            var email = query.Message.Email;
            var customer = await _customerRepository.GetAsync(email);

            if (customer == null)
            {
                throw new CustomerNotFoundApplicationException(email);
            }
            await query.RespondAsync<GetCustomerQueryResponse>(new GetCustomerQueryResponse(customer.FullName, customer.Age.Value, customer.Email.Value));
        }
    }
}
