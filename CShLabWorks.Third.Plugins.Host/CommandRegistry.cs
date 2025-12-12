using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Host;

public class CommandRegistry
{
    private readonly Dictionary<string, ICommand> _commands = [];

    public void Register(ICommand cmd)
    {
        var attrs = cmd
            .GetType()
            .GetCustomAttributes(typeof(CommandsAliasAttribute), true)
            .Cast<CommandsAliasAttribute>();

        foreach (var attr in attrs)
        {
            foreach(var alias in attr.Aliases)
            {
                if (_commands.TryGetValue(alias, out _))
                {
                    Console.WriteLine($"возник конфлик имен/алисов с {alias}");
                }

            }
        }
    }

    public ICommand? Find(string key)
        => _commands.TryGetValue(key, out var cmd) ? cmd : null;

    public IEnumerable<ICommand> GetAllCommands()
        => _commands.Values;
}

