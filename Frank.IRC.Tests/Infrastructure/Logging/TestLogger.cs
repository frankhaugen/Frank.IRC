using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

using NSubstitute;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure.Logging;

/// <summary>
/// Allows for testing of what is logged
/// </summary>
public class TestLogger : ILogger
{
    private readonly string _categoryName;
    private readonly ILoggerFormatter _formatter;
    private readonly ITestOutputHelper _outputHelper;

    public TestLogger(string categoryName, ITestOutputHelper outputHelper, ILoggerFormatter formatter)
    {
        _categoryName = categoryName;
        _outputHelper = outputHelper;
        _formatter = formatter;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return Substitute.For<IDisposable>();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        Dictionary<string, string> dictionary;

        try
        {
            dictionary = state is IReadOnlyList<KeyValuePair<string, object>> kvps
                ? kvps.ToDictionary(a => a.Key, a => a.Value.ToString())
                : new();
        }
        catch (Exception e)
        {
            dictionary = new();
        }

        var formattedMessage = _formatter.Format(logLevel, eventId, state, exception, formatter, _categoryName);
        TestLoggerMessages.Add(new TestLogEntry(DateTime.UtcNow, _categoryName, logLevel, eventId , formattedMessage.ToString(), dictionary));
        _outputHelper.WriteLine(formattedMessage.ToString());
    }
}