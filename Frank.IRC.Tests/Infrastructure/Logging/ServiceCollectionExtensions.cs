using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure.Logging
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers an ILoggerProvider that makes ILoggers that writes to test-output.
        /// </summary>
        public static IServiceCollection AddTestLogging(this IServiceCollection services, ITestOutputHelper output, ILoggerFormatter formatter = null)
        {
            if (output is null)
            {
                services.AddLogging(x => x.ClearProviders().AddProvider(NullLoggerProvider.Instance));
            }
            else
            {
                formatter ??= new TestLoggerFormatter();
                var provider = new TestLoggerProvider(output, formatter);
                services
                    .AddSingleton(output)
                    .AddLogging(x => x.AddProvider(provider));
            }

            return services;
        }
    }
}
