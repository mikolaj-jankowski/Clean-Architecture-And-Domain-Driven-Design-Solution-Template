using Clean.Architecture.And.DDD.Template.Infrastructure.Exceptions;
using FluentValidation;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class ValidationFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public ValidationFilter(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var _context = new ValidationContext<T>(context.Message);

            var validationFailures = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context.Message)));


            if (validationFailures.Any(x => x.Errors.Any()))
            {
                var groupedErrors = validationFailures.SelectMany(x => x.Errors).GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(a => a.ErrorMessage).ToArray());

                throw new CommandValidationException(String.Empty, groupedErrors);
            }

            await next.Send(context);

        }

        public void Probe(ProbeContext context) { }

    }

    
}
