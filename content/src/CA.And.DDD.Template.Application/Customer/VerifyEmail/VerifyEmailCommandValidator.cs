using CA.And.DDD.Template.Application.Customer.VerifyEmail;
using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>, IValidator
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(400);
        }
    }
}
