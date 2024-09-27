using FluentValidation;

namespace CA.And.DDD.Template.Application.Order.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>, IValidator
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(6);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Products).Must(i=> i != null && i.Count() > 0);
        }
    }
}
