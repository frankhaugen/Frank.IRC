namespace Frank.IRC;

public class IrcMessageNotValidException : Exception
{
    public IrcMessageNotValidException() : base("The message is not a valid IRC message.")
    {
    }
    
    public static void ThrowIfNotValid(Memory<byte> memory)
    {
        if (!IrcMessageHelper.IsIrcMessage(memory))
            throw new IrcMessageNotValidException();
    }
    
    public static void ThrowIfNotValid(ReadOnlyMemory<byte> memory)
    {
        if (!IrcMessageHelper.IsIrcMessage(memory))
            throw new IrcMessageNotValidException();
    }
}