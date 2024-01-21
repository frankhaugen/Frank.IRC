using Frank.BedrockSlim.Server;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.IRC.Server;

public static class HostBuilderExtensions
{
    public static IWebHostBuilder UseIrcServer(this IWebHostBuilder builder)
    {
        builder.UseTcpConnectionHandler<IrcMessageProcessor>(6667);
        builder.ConfigureServices(services =>
        {
            services.AddIrcServer();
        });
        return builder;
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIrcServer(this IServiceCollection services)
    {
        return services;
    }
}