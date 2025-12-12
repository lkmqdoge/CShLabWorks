using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Host;

public class CommandRegistry
{
    public Dictionary<string, ICommand> Commands { get; init; } = [];

    public void Register(Type cmd)
    {
        var attrs = cmd
            .GetCustomAttributes(typeof(CommandsAliasAttribute), true)
            .Cast<CommandsAliasAttribute>();

        var instance = Activator
            .CreateInstance(cmd);

        if (instance is ICommand commandInstance)
        {
            AddCommandName(commandInstance.Name, commandInstance);
            attrs
                .SelectMany(at => at.Aliases)
                .ToList()
                .ForEach(al => AddCommandName(al, commandInstance));
        }
    }

    public ICommand? Find(string key)
        => Commands.TryGetValue(key, out var cmd) ? cmd : null;

    public IEnumerable<ICommand> GetAllCommands()
        => Commands.Values;

    private void AddCommandName(string name, ICommand command)
    {
        if (Commands.TryGetValue(name, out _))
            Console.WriteLine($"возник конфлик имен/алисов с {name}");
        else
            Commands.Add(name, command);
    }
}

