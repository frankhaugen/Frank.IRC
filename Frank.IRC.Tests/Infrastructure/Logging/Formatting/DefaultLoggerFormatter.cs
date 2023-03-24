using System.Text;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.Formatting;

/// <summary>
/// The default logger formatter provided by Microsoft.Extensions.Logging.
/// </summary>
public class DefaultLoggerFormatter : ILoggerFormatter
{
    /// <inheritdoc />
    public StringBuilder Format<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, string categoryName)
    {
        var stringBuilder = new StringBuilder();
        
        stringBuilder.AppendLine(formatter(state, exception));

        return stringBuilder;
    }
}