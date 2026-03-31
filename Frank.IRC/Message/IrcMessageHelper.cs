namespace Frank.IRC;

public static class IrcMessageHelper
{
    public static bool IsIrcMessage(Memory<byte> memory)
    {
        ReadOnlySpan<byte> span = memory.Span;
        return IsIrcMessage(span);
    }
    
    public static bool IsIrcMessage(ReadOnlyMemory<byte> memory)
    {
        ReadOnlySpan<byte> span = memory.Span;
        return IsIrcMessage(span);
    }
    
    public static bool IsIrcMessage(ReadOnlySpan<byte> span)
    {
        if (span.Length < 2) return false; // Too short to be an IRC message

        // IRC messages should end with '\r\n'
        return span[0] == ':' && span[^2] == '\r' && span[^1] == '\n';
    }
}