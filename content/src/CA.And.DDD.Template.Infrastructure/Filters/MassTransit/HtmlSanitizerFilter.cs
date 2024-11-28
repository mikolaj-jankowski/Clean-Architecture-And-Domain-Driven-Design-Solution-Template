using Ganss.Xss;
using MassTransit;

namespace CA.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class HtmlSanitizerFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        public HtmlSanitizerFilter()
        {
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var sanitizer = new HtmlSanitizer();

            var originalMessage = context.Message.ToString();

            var sanitized = sanitizer.Sanitize(context.Message.ToString());

            if (!originalMessage.Equals(sanitized))
            {
                throw new ArgumentException("The provided content contains potentially dangerous scripts.");
            }

            await next.Send(context);
        }


        public void Probe(ProbeContext context) { }
    }
}
