using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure.Logging
{
    public class TestLoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, TestLogger> Loggers = new();
        private TestLoggerProvider _provider;

        public TestLoggerFactory(ITestOutputHelper output, ILoggerFormatter formatter)
        {
            _provider = new TestLoggerProvider(output, formatter);
        }
        
        public void AddProvider(ILoggerProvider provider)
        {
            if (provider is TestLoggerProvider loggerProvider)
                _provider = loggerProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (Loggers.TryGetValue(categoryName, out var logger)) return logger;
            logger = _provider.CreateLogger(categoryName) as TestLogger;
            Loggers[categoryName] = logger;
            return logger;
        }

        public void Dispose()
        { }
    }
}
