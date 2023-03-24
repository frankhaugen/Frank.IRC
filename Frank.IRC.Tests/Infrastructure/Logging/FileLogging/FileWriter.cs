using System.Collections.Concurrent;

namespace Frank.IRC.Tests.Infrastructure.Logging.FileLogging;

internal class FileWriter
{
    private static readonly ConcurrentDictionary<string, FileWriter> _fileWriters = new();

    private readonly ReaderWriterLock _locker = new();
    private readonly string _filename;

    private FileWriter(string filename)
    {
        _filename = filename;
    }

    internal static FileWriter GetFor(FileInfo fileInfo)
    {
        // The FileLoggerProvider create the directory if needed.
        var fullName = fileInfo.FullName;

        if (_fileWriters.TryGetValue(fullName, out var fileWriter))
        {
            return fileWriter;
        }

        if (!File.Exists(fullName))
        {
            using var _ = fileInfo.Create();
        }

        fileWriter = new FileWriter(fullName);
        if (_fileWriters.TryAdd(fullName, fileWriter))
        {
            fileWriter.WriteLine($"▶ Starting at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}.");
            return fileWriter;
        }

        return _fileWriters[fullName];
    }

    internal static void WriteStoppedInAllFiles()
    {
        foreach (var fileWriter in _fileWriters.Values)
        {
            fileWriter.WriteLine($"⏹ Stopped at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}.");
            fileWriter.WriteLine($"___");
        }
    }

    public void WriteLine(string line) => WriteLines(line);
    
    public void WriteLines(params string[] lines)
    {
        try
        {
            _locker.AcquireWriterLock(int.MaxValue);

            File.AppendAllLines(_filename, lines);
        }
        finally
        {
            _locker.ReleaseWriterLock();
        }
    }
}