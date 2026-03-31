namespace Frank.IRC;

public class IrcMessageParserService : IIrcMessageParserService
{
    public IrcMessage Parse(ReadOnlyMemory<byte> memory)
    {
        IrcMessageNotValidException.ThrowIfNotValid(memory);
        var message = new IrcMessage(memory);
        return message;
    }
}

public interface IIrcMessageParserService
{
    IrcMessage Parse(ReadOnlyMemory<byte> memory);
}