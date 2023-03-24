using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging;

public record TestLogEntry(DateTime DateTime, string CategoryName, LogLevel LogLevel, EventId EventId, string Message, Dictionary<string, string> Dictionary);