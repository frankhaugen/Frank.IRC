using System.Text;
using System.Text.RegularExpressions;

using Frank.IRC.Tests.Infrastructure.Logging.FileLogging;

using Microsoft.Extensions.Logging;

namespace Frank.IRC.Tests.Infrastructure.Logging.Formatting;

/// <summary>
/// This formatter is used to format the log messages to markdown and is used by the <see cref="FileLogger"/>.
/// </summary>
public class MarkdownLoggerFormatter : ILoggerFormatter
{
    /// <inheritdoc />
    public StringBuilder Format<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, string categoryName)
    {
        var stringBuilder = new StringBuilder();
        var message = state switch
        {
            IReadOnlyList<KeyValuePair<string, object>> kvp => KeyValuePairsToString(kvp),
            _ => state.ToString()
        };

        var now = DateTime.UtcNow;
        
        var displayLoggerName = categoryName.StartsWith("Semine") || categoryName.StartsWith("Bilagos")
            ? Regex.Replace(categoryName, @"\w+$", m => $"__{m.Value}__", RegexOptions.RightToLeft)
            : categoryName;

        var level = logLevel switch
        {
            LogLevel.Critical => "💣",
            LogLevel.Error => "❌",
            LogLevel.Warning => "⚠",
            LogLevel.Information => "ℹ",
            LogLevel.Debug => "🧐",
            _ => "[Other]"
        };

        var formattedMessage = FormattedMessage(message);
        
        stringBuilder.AppendLine($"### `{now:s}` {level} {displayLoggerName}");
        stringBuilder.AppendLine(formattedMessage);
        if (exception != null)
        {
            stringBuilder.AppendLine(exception.ToMarkdown());
        }

        return stringBuilder;
    }

    private static string KeyValuePairsToString(IReadOnlyList<KeyValuePair<string, object>> kvp)
    {
        var format = kvp.Last().Value.ToString();
        var isMultiline = format.Contains("\r\n") || kvp.Any(a => a.Value is "\r\n"); // Entity-framework has {newline} as parameter.

        var dict = kvp.SkipLast(1).ToDictionary(kvp => kvp.Key, kvp =>
            isMultiline ? kvp.Value?.ToString() :
            kvp.Value == null ? "" :
            kvp.Value is string s && Regex.Match(s, @"^\s+|\s+$|^$").Success ? s :
            $"**{kvp.Value}**"
        );

        var keysPattern = string.Join("|", dict.Keys.Select(a => Regex.Replace(a, @"[\\\^\$\.\|\?\*\+\(\)*[\\]\{\}]", m => @$"\{m.Value}"))); // Escape special characters in regex

        return Regex.Replace(format, @$"\{{(?<key>{keysPattern})\}}", m => dict.TryGetValue(m.Groups["key"].Value, out var val) ? val : $"-did not find key {m.Groups["key"].Value}-");
    }

    private static string FormattedMessage(string message)
    {
        var quotedValuePattern = @"(?<=([\=\:]\s?))(['`""])((?:(?!\2).){1,40})\2"; // https://stackoverflow.com/a/8057827/470022
        var isSql = message.Contains("Executed DbCommand");
        if (isSql)
        {
            var split = message.Split(new[] { "\r\n" }, StringSplitOptions.None);
            split[0] = Regex.Replace(split[0], quotedValuePattern, m => $"{m.Groups[2].Value}**{FormattedValue(m.Groups[3].Value).Trim()}**{m.Groups[2].Value}"); // **bold** all non-empty values listed in first sql-line.
            split[0] = split[0].Replace("CommandType='**Text**', CommandTimeout='**30**'", "CommandType='Text', CommandTimeout='30'"); // Unhighlight defaults
            message = string.Join("\r\n", split);
        }

        message = Regex.Replace(message, @"\bNULL\b", "`NULL`");
        var guidPattern = "[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}";
        var nonQuotedQuid = @$"(?<!`){guidPattern}(?!`)";
        message = Regex.Replace(message, nonQuotedQuid, m => $"`{FormattedValue(m.Value)}`");
        message = Regex.Replace(message, @"Microsoft.Data.SqlClient.SqlException[^\r]*", m => $"__{m.Value}__"); // Highlight SqlException
        message = message.Replace("\r\n", "  \r\n"); // Add to two spaces at end of lines -> Show line-breaks in markdown too.
        if (!message.Contains("```")) // Don't make code blocks if message alreay has codeblocks.
        {
            var startCodeBlockAt = Math.Max(message.IndexOf("The statement has been terminated"), message.IndexOf("\r\n") + 2);
            if (startCodeBlockAt >= 10)
            {
                // message = message.Insert(startCodeBlockAt + 2, ">"); // Block-quote all after first line of message.
                message = message.Insert(message.Length, "\r\n```").Insert(startCodeBlockAt, isSql ? "``` sql\r\n" : "```\r\n");
            }
        }

        message = Regex.Replace(message, @"(\w)`(\d \b)", m => $"{m.Groups[1].Value}'{m.Groups[2].Value}"); // Replace back-ticks in generic types for correct markdown
        return message;
    }

    private static string FormattedValue(string input)
    {
        if (Guid.TryParse(input, out var guid))
        {
            if (!guids.TryGetValue(guid, out var abr))
            {
                var guidString = Regex.Replace(guid.ToString("N"), @"(^0{3,}|0{3,}$)", ""); // Three zeroes at an end can't be a coincidence, ignore :)

                if (guid == Guid.Empty)
                {
                    abr = "💠0";
                }
                else if (guidString.Length < 7)
                {
                    abr = $"🔸{guidString}";
                }
                else
                {
                    var take = Math.Min(guidString.Length, 3);
                    abr = guidString.Substring(0, take);
                    while (guidsRev.TryGetValue(abr, out var other))
                    {
                        do
                        {
                            take++;
                        } while (take < guidString.Length && take < other.Length && guidString[take] == other[take]);

                        abr = guidString.Substring(0, take);
                    }

                    abr = $"🔹{abr}";
                }

                guidsRev[abr] = guidString;
                guids[guid] = abr;
            }

            return abr;
        }

        return input;
    }

    private static Dictionary<Guid, string> guids = new Dictionary<Guid, string>();
    private static Dictionary<string, string> guidsRev = new Dictionary<string, string>();
}