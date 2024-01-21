using Frank.BedrockSlim.Server;

namespace Frank.IRC.Server;

public class IrcMessageProcessor : IConnectionProcessor
{
    public async Task<ReadOnlyMemory<byte>> ProcessAsync(ReadOnlyMemory<byte> input)
    {
        await Task.CompletedTask;
        return input;
    }
}