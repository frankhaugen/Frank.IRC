using System.Reflection;

using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.FileLogging;

public class FileLogger : ILogger
{
    private readonly FileWriter _fileWriter;
    private readonly string _loggerName;
    private readonly ILoggerFormatter _formatter;

    public FileLogger(ILoggerFormatter formatter, string categoryName, DirectoryInfo directory)
    {
        _formatter = formatter;
        _loggerName = categoryName;
        
        var fileName = $"{Assembly.GetCallingAssembly().GetName().Name}.log.md";
        var fileInfo = new FileInfo(Path.Combine(directory.FullName, fileName));
        _fileWriter = FileWriter.GetFor(fileInfo);
    }

    public IDisposable BeginScope<TState>(TState state) => new FileLoggerScope();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var formattedMessage = _formatter.Format(logLevel, eventId, state, exception, formatter, _loggerName);
        _fileWriter.WriteLine(formattedMessage.ToString());
    }
}