using FluentValidation;

namespace CA.And.DDD.Template.Application.Order.GetOrder
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>, IValidator
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
