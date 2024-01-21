using System.Text;

namespace Frank.IRC.Client;

public class IrcMessageBuilder
{
    private string prefix;
    private string command;
    private List<string> parameters = new List<string>();

    public IrcMessageBuilder WithPrefix(string prefix)
    {
        this.prefix = prefix;
        return this;
    }

    public IrcMessageBuilder WithCommand(string command)
    {
        this.command = command;
        return this;
    }

    public IrcMessageBuilder AddParameter(string parameter)
    {
        this.parameters.Add(parameter);
        return this;
    }

    public IrcMessage Build()
    {
        StringBuilder message = new StringBuilder();
        if (!string.IsNullOrEmpty(prefix))
        {
            message.Append(':').Append(prefix).Append(' ');
        }
        
        message.Append(command);

        foreach (var param in parameters)
        {
            message.Append(' ').Append(param);
        }

        message.Append("\r\n");
        return new IrcMessage(Encoding.UTF8.GetBytes(message.ToString()));
    }
}