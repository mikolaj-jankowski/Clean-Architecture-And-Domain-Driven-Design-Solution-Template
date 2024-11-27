using FluentValidation;

namespace CA.And.DDD.Template.Application.Customer.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>, IValidator
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
