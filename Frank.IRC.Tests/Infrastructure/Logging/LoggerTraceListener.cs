using System.Collections.Concurrent;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging;

public class LoggerTraceListener : TraceListener, IDisposable
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ConcurrentDictionary<string, ILogger> _loggers = new();

    public LoggerTraceListener(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public override void Write(string message) => Logger.LogInformation(message);

    public override void WriteLine(string message) => Logger.LogInformation(message);

    public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
    {
        //new { eventCache, source, eventType, id, format, args }.Dump();
        GetLogger(source).LogDebug(format, args); 
    }

    private static LogLevel ToLogLevel(TraceEventType eventType) =>
        eventType switch
        {
            TraceEventType.Critical => LogLevel.Critical,
            TraceEventType.Error => LogLevel.Error,
            TraceEventType.Warning => LogLevel.Warning,
            TraceEventType.Information => LogLevel.Information,
            TraceEventType.Verbose => LogLevel.Debug,
            TraceEventType.Start => LogLevel.Trace,
            TraceEventType.Stop => LogLevel.Trace,
            TraceEventType.Suspend => LogLevel.Trace,
            TraceEventType.Resume => LogLevel.Trace,
            TraceEventType.Transfer => LogLevel.Trace,
            _ => throw new NotSupportedException(),
        };

    ILogger _logger;
    public ILogger Logger { get { return _logger ??= GetLogger(nameof(LoggerTraceListener)); } }
    private ILogger GetLogger(string source)
    {
        if (!_loggers.TryGetValue(source, out var logger))
        {
            logger = _loggerFactory.CreateLogger(source);
            if (!_loggers.TryAdd(source, logger) && !_loggers.TryGetValue(source, out logger))
            {
                throw new InvalidOperationException("Could not add logger but not get existing either.");
            }
        }

        return logger;
    }

    public void Dispose()
    {
        base.Dispose();

        if (_loggerFactory != null)
        {
            _loggerFactory.Dispose();
        }
    }
}
