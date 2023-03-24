using System.Collections;
using System.Text;

public static class ExceptionExtensions
{
    /// <summary>
    /// Converts an exception to a markdown string for use in test output or logging.
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="includeStacktrace"></param>
    /// <param name="includeData"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToMarkdown<T>(this T exception, bool includeStacktrace = true, bool includeData = true) where T : Exception
    {
        var markdownBuilder = new StringBuilder();

        markdownBuilder.AppendLine($"#### {exception.GetType().Name}");
        foreach (var messageLine in exception.Message.Split(Environment.NewLine)) markdownBuilder.AppendLine($"> {messageLine}");

        if (includeStacktrace) markdownBuilder = AppendStackTrace(markdownBuilder, exception);
        if (includeData) markdownBuilder = AppendData(markdownBuilder, exception);
        
        return markdownBuilder.ToString();
    }
    
    private static StringBuilder AppendStackTrace(StringBuilder markdownBuilder, Exception exception)
    {
        if (exception.StackTrace is null) return markdownBuilder;
        markdownBuilder.AppendLine("```C#");
        foreach (var frame in exception.StackTrace.Split(Environment.NewLine))
            markdownBuilder.AppendLine($"    {frame}");
        markdownBuilder.AppendLine("```");
        return markdownBuilder;
    }
    
    private static StringBuilder AppendData(StringBuilder markdownBuilder, Exception exception)
    {
        if (exception.Data is not { Count: > 0 }) return markdownBuilder;
        markdownBuilder.AppendLine($"|Key|Value|");
        markdownBuilder.AppendLine($"|---|---|");
        foreach (DictionaryEntry entry in exception.Data) markdownBuilder.AppendLine($"|{entry.Key}|{entry.Value}|");
        return markdownBuilder;
    } 
}