using System.Net;
using System.Net.Sockets;
using System.Text;

using Frank.IRC.Models;

namespace Frank.IRC.Server;

public class IrcListener
{
    public async Task Listen()
    {
        var listener = new TcpListener(IPAddress.Any, 6667);
        listener.Start();

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            var stream = client.GetStream();

            var buffer = new byte[1024];
            var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var ircMessage = ParseMessage(message);

        }
    }

    private IrcMessage ParseMessage(string message)
    {
        var parts = message.Split(' ');
        var command = parts[0];
        var parameters = parts.Skip(1).ToArray();
        return new IrcMessage();
    }
}