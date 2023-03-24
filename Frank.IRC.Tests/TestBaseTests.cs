using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Frank.IRC.Tests;

public class TestBaseTests : TestBase
{
    public TestBaseTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public async Task TestBaseTest()
    {
        _outputHelper.WriteLine("Hello World");
        
        var services = new ServiceCollection();
        services.AddHostedService<TestService>();
        
        await CreateAndRunAsync();
        
        _outputHelper.WriteLine("Goodbye World");
    }
    
    private class TestService : BackgroundService
    {
        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hello World from TestService");
        }
    }
}