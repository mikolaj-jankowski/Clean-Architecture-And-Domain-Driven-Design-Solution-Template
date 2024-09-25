using Clean.Architecture.And.DDD.Template.Application.Customer.GetCustomer;
using FluentValidation;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>, IValidator
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(400);
        }
    }
}
