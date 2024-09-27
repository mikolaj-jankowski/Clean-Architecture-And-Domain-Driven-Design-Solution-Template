using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Domian.Customers;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Queries.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly AppDbContext _appDbContext;

        public GetCustomerQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Consume(ConsumeContext<GetCustomerQuery> query)
        {
            var email = query.Message.Email;
            var customer = await _appDbContext.Set<Customer>().Where(x => ((string)x.Email).Contains(email)).SingleOrDefaultAsync();

            if (customer == null)
            {
                throw new CustomerNotFoundApplicationException(email);
            }
            await query.RespondAsync(new GetCustomerQueryResponse(customer.FullName, customer.Age.Value, customer.Email.Value));
        }
    }
}
