using CA.And.DDD.Template.Domain.Orders;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.GetCustomer
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>, IValidator
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(CustomerConstants.Customer.EmailMaxLength);
        }
    }
}
