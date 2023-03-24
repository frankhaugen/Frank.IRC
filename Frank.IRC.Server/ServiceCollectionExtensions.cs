using Frank.IRC.Networking;
using Frank.IRC.Networking.Sockets;

namespace Frank.IRC.Server;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSocketServer(this IServiceCollection services)
    {
        services.AddHostedService<SocketListener>();
        return services;
    }
    
    internal static IServiceCollection AddSocketMessageQueue(this IServiceCollection services)
    {
        services.AddSingleton<ISocketMessageQueue, SocketMessageQueue>();
        services.AddHostedService<SocketMessageQueueProcessor>();
        return services;
    }
    
    internal static IServiceCollection AddSocketMessageHandler(this IServiceCollection services)
    {
        services.AddSingleton<ISocketMessageHandler, GenericHandler>();
        return services;
    }
    
    internal static IServiceCollection AddSocketMessageHandler<T>(this IServiceCollection services)
        where T : class, ISocketMessageHandler
    {
        services.AddSingleton<ISocketMessageHandler, T>();
        return services;
    }
}