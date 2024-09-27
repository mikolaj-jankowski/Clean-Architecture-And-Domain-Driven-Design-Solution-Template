using CA.And.DDD.Template.Application.Customer.ChangeEmail;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>, IValidator
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(x => x.OldEmail).NotEmpty().MaximumLength(400);
            RuleFor(x => x.NewEmail).NotEmpty().MaximumLength(400);
        }
    }
}
