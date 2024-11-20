using CA.And.DDD.Template.Domain.Orders;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.ChangeEmail
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>, IValidator
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(x => x.OldEmail).NotEmpty().MaximumLength(CustomerConstants.Customer.EmailMaxLength);
            RuleFor(x => x.NewEmail).NotEmpty().MaximumLength(CustomerConstants.Customer.EmailMaxLength);
        }
    }
}
