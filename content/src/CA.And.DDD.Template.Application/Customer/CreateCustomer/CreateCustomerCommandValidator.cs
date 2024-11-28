using CA.And.DDD.Template.Domain.Orders;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>, IValidator
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(CustomerConstants.Customer.EmailMaxLength);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(CustomerConstants.Customer.FullNameMaxLength).MinimumLength(CustomerConstants.Customer.FullNameMinLength);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(CustomerConstants.Customer.StreetMaxLength);
            RuleFor(x => x.HouseNumber).NotEmpty().MaximumLength(CustomerConstants.Customer.HouseNumberMaxLength);
            RuleFor(x => x.FlatNumber).NotEmpty().MaximumLength(CustomerConstants.Customer.FlatNumberMaxLength);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(CustomerConstants.Customer.CountryMaxLength);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(CustomerConstants.Customer.PostalCodeMaxLength);
        }
    }
}
