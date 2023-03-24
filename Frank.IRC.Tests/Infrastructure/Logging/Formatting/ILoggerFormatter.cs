using System.Text;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.Formatting;

public interface ILoggerFormatter
{
    /// <summary>
    /// Formats the log entry into a string builder.
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="state"></param>
    /// <param name="exception"></param>
    /// <param name="formatter"></param>
    /// <param name="categoryName"></param>
    /// <typeparam name="TState"></typeparam>
    /// <returns></returns>
    StringBuilder Format<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, string categoryName);
}