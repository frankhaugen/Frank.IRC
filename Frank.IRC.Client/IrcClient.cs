using System.Net;

using Frank.BedrockSlim.Client;

using Microsoft.Extensions.Options;

namespace Frank.IRC.Client;

public class IrcClient
{
    private readonly ITcpClient _tcpClient;
    private readonly IOptions<IrcClientOptions> _options;
    
    public IrcClient(ITcpClient tcpClient, IOptions<IrcClientOptions> options)
    {
        _tcpClient = tcpClient;
        _options = options;
    }
    
    public async Task LoginAsync()
    {
        var message = new IrcMessageBuilder();
        
        if (!string.IsNullOrEmpty(_options.Value.Password))
        {
            message.WithCommand("PASS");
            message.AddParameter(_options.Value.Password);
        }
        
        message.WithCommand("NICK");
        message.AddParameter(_options.Value.Nickname);
        
        message.WithCommand("USER");
        message.AddParameter(_options.Value.Username);
        
        message.AddParameter("0");
        message.AddParameter("*");
        message.AddParameter(_options.Value.Realname);
        
        await SendAsync(message.Build());
    }
    
    public async Task<IrcMessage> SendAsync(IrcMessage message)
    {
        var hostEntry = await Dns.GetHostEntryAsync(_options.Value.Host);
        var ipAddress = hostEntry.AddressList.First();
        var response = await _tcpClient.SendAsync(ipAddress, _options.Value.Port,message.ToMemory());
        return new IrcMessage(response);
    }
}

public class IrcClientOptions
{
    public string Host { get; set; } = "irc.freenode.net";
    public int Port { get; set; } = 6667;
    public string Nickname { get; set; } = "Frank";
    public string Username { get; set; } = "Frank";
    public string Realname { get; set; } = "Frank";
    public string Password { get; set; } = "";
}