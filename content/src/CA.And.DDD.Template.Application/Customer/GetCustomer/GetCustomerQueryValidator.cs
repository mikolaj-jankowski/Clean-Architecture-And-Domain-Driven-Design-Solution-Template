using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.GetCustomer
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>, IValidator
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(400);
        }
    }
}
