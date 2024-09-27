using CA.And.DDD.Template.Domian.Customers;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>, IValidator
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(400);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(55);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(255);
            RuleFor(x => x.HouseNumber).NotEmpty().MaximumLength(15);
            RuleFor(x => x.FlatNumber).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(6);
        }
    }
}
