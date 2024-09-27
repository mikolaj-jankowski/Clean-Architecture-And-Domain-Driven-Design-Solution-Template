using MassTransit;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CA.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly ILogger<LoggingFilter<T>> _logger;

        public LoggingFilter(ILogger<LoggingFilter<T>> logger)
        {
            _logger = logger;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await next.Send(context);
            }
            finally
            {
                stopwatch.Stop();

            }
            _logger.LogTrace($"Operation duration: {stopwatch.Elapsed.TotalMilliseconds} ms", context);

        }


        public void Probe(ProbeContext context) { }
    }
}
