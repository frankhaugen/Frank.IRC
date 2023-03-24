using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

using Frank.IRC.Tests.Infrastructure.Logging;
using Frank.IRC.Tests.Infrastructure.Logging.Formatting;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Frank.IRC.Tests.Infrastructure;

public static class TestOutputHelperExtensions
{
    private static JsonSerializerOptions GetJsonSerializerOptions(bool writeIndented = true)
    {
        var jsonSerializationOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = writeIndented,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        jsonSerializationOptions.Converters.Add(new JsonStringEnumConverter());
        return jsonSerializationOptions;
    }
    
    /// <summary>
    /// Creates a Logger backed by <see cref="TestLoggerProvider"/>
    /// </summary>
    /// <param name="outputHelper"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ILogger<T> GetLogger<T>(this ITestOutputHelper outputHelper) => outputHelper.GetLoggerFactory().CreateLogger<T>();
    
    /// <summary>
    /// Creates a Logger backed by <see cref="TestLoggerProvider"/>
    /// </summary>
    /// <param name="outputHelper"></param>
    /// <param name="categoryName">Name of the logger</param>
    /// <returns></returns>
    public static ILogger GetLogger(this ITestOutputHelper outputHelper, string categoryName) => outputHelper.GetLoggerFactory().CreateLogger(categoryName);

    /// <summary>
    /// Gets an instance of <see cref="ILoggerProvider"/> backed by <see cref="TestLoggerProvider"/>
    /// </summary>
    /// <param name="outputHelper"></param>
    /// <returns></returns>
    public static ILoggerProvider GetLoggerProvider(this ITestOutputHelper outputHelper) => new TestLoggerProvider(outputHelper, new TestLoggerFormatter());

    /// <summary>
    /// Gets an instance of <see cref="ILoggerFactory"/> backed by <see cref="TestLoggerFactory"/>
    /// </summary>
    /// <param name="outputHelper"></param>
    /// <returns>A Logger factory</returns>
    public static ILoggerFactory GetLoggerFactory(this ITestOutputHelper outputHelper) => new TestLoggerFactory(outputHelper, new TestLoggerFormatter());

    /// <summary>
    /// Serialize and write out the value in Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="writeIndented"></param>
    public static void WriteLine<T>(this ITestOutputHelper source, T value, bool writeIndented = true) => source.WriteLine(JsonSerializer.Serialize(value, GetJsonSerializerOptions(writeIndented)));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="serializerOptions"></param>
    public static void WriteLine<T>(this ITestOutputHelper source, T value, JsonSerializerOptions serializerOptions) => source.WriteLine(JsonSerializer.Serialize(value, serializerOptions));

    /// <summary>
    /// Writes JSON with a self-defined JsonSerializerOptions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="serializerOptions"></param>
    public static void WriteJson<T>(this ITestOutputHelper source, T value, JsonSerializerOptions serializerOptions) => source.WriteLine(JsonSerializer.Serialize(value, serializerOptions));
}