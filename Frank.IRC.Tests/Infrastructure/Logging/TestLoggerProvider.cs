using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure.Logging;

public class TestLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _output;
    private readonly Dictionary<string, TestLogger> _testLoggers  = new();
    private readonly ILoggerFormatter _formatter;
    
    public TestLoggerProvider(ITestOutputHelper output, ILoggerFormatter formatter)
    {
        _output = output;
        _formatter = formatter;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_testLoggers.TryGetValue(categoryName, out var logger)) return logger;
        logger = new TestLogger(categoryName, _output, _formatter);
        _testLoggers[categoryName] = logger;
        return logger;
    }

    public void Dispose()
    {
        TestLoggerMessages.Clear();
    }
}