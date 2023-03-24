using System.Globalization;
using System.Text;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.Formatting;

/// <summary>
/// this class is used to format the log messages and is taken from Divergic.Logging.Xunit project on GitHub
/// </summary>
public class TestLoggerFormatter : ILoggerFormatter
{
    /// <inheritdoc />
    public StringBuilder Format<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, string categoryName)
    {
        var message = formatter(state, exception);
        var padding = new string(' ', 2);
        var stringBuilder = new StringBuilder();

        if (string.IsNullOrWhiteSpace(message) == false)
        {
            var part = string.Format(CultureInfo.InvariantCulture, FormatMask, padding, logLevel, eventId.Id, message);
            stringBuilder.AppendLine(part);
        }

        if (exception != null)
        {
            var part = string.Format(
                CultureInfo.InvariantCulture,
                FormatMask,
                padding,
                logLevel,
                eventId.Id,
                exception);

            stringBuilder.AppendLine(part);
        }

        return stringBuilder;
    }

    /// <summary>
    /// Returns the string format mask used to generate a log message.
    /// </summary>
    /// <remarks>The format values are:
    /// <ul>
    ///     <li>0: Padding</li>
    ///     <li>1: Level</li>
    ///     <li>2: Event Id</li>
    ///     <li>3: Message</li>
    /// </ul>
    /// </remarks>
    private string FormatMask = "{0}{1} [{2}]: {3}";
}