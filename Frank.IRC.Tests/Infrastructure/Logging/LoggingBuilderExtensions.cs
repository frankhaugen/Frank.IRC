using Frank.IRC.Tests.Infrastructure.Logging.FileLogging;
using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure.Logging;

public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Adds a test logger to the logging builder. The test logger will write to the test output.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="outputHelper"></param>
    /// <param name="formatter"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddTestLogger(this ILoggingBuilder builder, ITestOutputHelper outputHelper, ILoggerFormatter formatter = null)
    {
        formatter ??= new TestLoggerFormatter();
        var provider = new TestLoggerProvider(outputHelper, formatter);
        builder.AddProvider(provider);
        builder.Services.AddSingleton(outputHelper);
        return builder;
    }
    
    /// <summary>
    /// Adds a file logger to the logging builder. The file logger will write to the specified directory.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="directory"></param>
    /// <param name="formatter"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, DirectoryInfo directory, ILoggerFormatter formatter = null)
    {
        formatter ??= new MarkdownLoggerFormatter();
        builder.AddProvider(new FileLoggerProvider(directory, formatter));
        return builder;
    }
}