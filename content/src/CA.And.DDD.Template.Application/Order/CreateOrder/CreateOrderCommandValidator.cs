using CA.And.DDD.Template.Domain.Orders;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Order.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>, IValidator
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(OrderConstants.Order.PostalCodeMaxLength);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(OrderConstants.Order.StreetMaxLength);
            RuleFor(x => x.Products).Must(i => i != null && i.Count() > 0);
        }
    }
}
