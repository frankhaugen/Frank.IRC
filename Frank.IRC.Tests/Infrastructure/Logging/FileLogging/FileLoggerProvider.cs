using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.FileLogging;

public class FileLoggerProvider : ILoggerProvider, IDisposable
{
    private readonly DirectoryInfo _directory;
    private readonly ILoggerFormatter _formatter;
    
    public FileLoggerProvider(DirectoryInfo directory)
    {
        _directory = directory;
        _formatter = new MarkdownLoggerFormatter();
        if (!_directory.Exists) _directory.Create();
    }
    
    public FileLoggerProvider(DirectoryInfo directory, ILoggerFormatter formatter)
    {
        _directory = directory;
        _formatter = formatter;
        if (!_directory.Exists) _directory.Create();
    }

    public void Dispose()
    {
        FileWriter.WriteStoppedInAllFiles();
    }

    public ILogger CreateLogger(string categoryName) => new FileLogger(_formatter, categoryName, _directory);
}