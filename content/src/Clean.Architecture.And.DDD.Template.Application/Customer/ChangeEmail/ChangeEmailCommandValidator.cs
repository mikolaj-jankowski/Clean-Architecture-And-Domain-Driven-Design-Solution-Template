using Clean.Architecture.And.DDD.Template.Application.Customer.ChangeEmail;
using FluentValidation;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
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
