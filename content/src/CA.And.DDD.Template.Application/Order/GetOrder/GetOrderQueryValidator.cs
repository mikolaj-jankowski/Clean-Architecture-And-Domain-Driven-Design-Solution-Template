using CA.And.DDD.Template.Application.Customer.GetCustomer;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>, IValidator
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
