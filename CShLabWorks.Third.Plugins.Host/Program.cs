using CShLabWorks.Third.Plugins.Host;

if (args.Length != 1)
{
    const string m =
        """
        Usage:
            -p <path-to-plugins-folder>
        """;
    Console.WriteLine(m);
    return;
}

var dirPath = args[0];
var path = Path.Combine(Environment.CurrentDirectory, dirPath);

var p = new PluginStorage();
p.LoadPlugins(path);

var r = new CommandRegistry();

while (true)
{
    Console.Write("> ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var command = parts[0].ToLower();

    if (command == "exit")
        break;

    switch (command)
    {
        case "run":
            if (parts.Length < 2)
            {
                Console.WriteLine("Использование: run <команда> [аргументы]");
                break;
            }

            string key = parts[1];
            ICommand? cmd = registry.Find(key);

            if (cmd == null)
            {
                Console.WriteLine($"Команда '{key}' не найдена.");
                break;
            }

            var args = parts.Skip(2).ToArray();

            var sw = Stopwatch.StartNew();
            int result = cmd.Execute(args);
            sw.Stop();

            Console.WriteLine($"Код возврата: {result}");
            Console.WriteLine($"Время: {sw.ElapsedMilliseconds} мс");
            break;

        case "help":
            if (parts.Length == 1)
            {
                Console.WriteLine("Доступные команды:");

                var groups = registry.AllCommands()
                    .GroupBy(t => t.Value)
                    .Select(g => new
                    {
                        Cmd = g.Key,
                        Keys = g.Select(x => x.key).ToList()
                    });

                foreach (var g in groups)
                {
                    Console.WriteLine($"- {g.Cmd.Name}: {g.Cmd.Description}");
                    Console.WriteLine($"  Алиасы: {string.Join(", ", g.Keys.Where(x => x != g.Cmd.Name))}");
                }
                break;
            }
            else
            {
                string name = parts[1];
                var cmd2 = registry.Find(name);

                if (cmd2 == null)
                {
                    Console.WriteLine("Команда не найдена.");
                    break;
                }

                var keys = registry.AllCommands()
                    .Where(x => x.cmd == cmd2)
                    .Select(x => x.key);

                Console.WriteLine($"Команда: {cmd2.Name}");
                Console.WriteLine($"Описание: {cmd2.Description}");
                Console.WriteLine($"Алиасы: {string.Join(", ", keys.Where(k => k != cmd2.Name))}");
            }
            break;

        default:
            Console.WriteLine("Неизвестная команда. Используйте help.");
            break;
    }
}
