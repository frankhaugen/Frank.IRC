namespace Frank.IRC.Tests.Infrastructure.Logging;

public static class TestLoggerMessages
{
    private static readonly List<TestLogEntry> Logged = new();
    
    public static void Add(TestLogEntry entry) => Logged.Add(entry);

    public static void Clear() => Logged.Clear();

    public static IReadOnlyList<string> GetCategories() => Logged.Select(x => x.CategoryName).Distinct().ToList();
    public static IReadOnlyList<TestLogEntry> Get() => Logged;
    public static IReadOnlyList<TestLogEntry> Get<T>() => Logged.Where(x => x.CategoryName == typeof(T).FullName).ToList();
}