using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Host;

public class CommandHost
{
    private readonly PluginStorage _pluginStorage = new();

    private readonly CommandRegistry _commandRegisty = new ();

    private bool _isRunning = true;

    private int _lastExitCode = int.MaxValue;

    public CommandHost(string pluginsDirPath)
    {
        _pluginStorage = new();
        _pluginStorage.LoadPlugins(pluginsDirPath);

        foreach (var command in _pluginStorage.CommandTypes)
        {
            _commandRegisty.Register(command);
        }
    }

    public void Run()
    {
        while (_isRunning)
        {
            var code = _lastExitCode == int.MaxValue 
                ? ""
                : _lastExitCode.ToString();

            Console.Write($"{code}#> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch(parts[0].ToLower())
            {
                case "help": PrintHelp(parts); break;
                case "run" : RunCommand(parts); break;
                case "q":
                case "exit": Exit(); break;
            }
        }
    }

    private void PrintHelp(params string[] args)
    {
        var commands = _commandRegisty.GetAllCommands().Distinct();

        if (args.Length < 2)
        {
        commands.ToList()
            .ForEach(PrintCommandHelp);
        }
        else
        {
            var commandName = args[1];
            if (_commandRegisty.Commands.TryGetValue(commandName, out var c))
                PrintCommandHelp(c);
        }
    }

    private void PrintCommandHelp(ICommand c)
    {
        Console.WriteLine(c.Name);
        Console.WriteLine("Описание:");
        Console.WriteLine(c.Description);
        Console.Write("алиасы: ");
        _commandRegisty.Commands
            .Where(p => p.Value == c)
            .Select(p => p.Key)
            .ToList()
            .ForEach(al => Console.Write($"{al} "));
        Console.WriteLine('\n');
    }

    private void RunCommand(params string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Укажите комманду");
            return;
        }

        var commandName = args[1];
        if (_commandRegisty.Commands.TryGetValue(commandName, out var c))
            _lastExitCode = c.Execute([.. args.Skip(2)]);
    }

    private void Exit()
    {
        _isRunning = false;
    }
}

