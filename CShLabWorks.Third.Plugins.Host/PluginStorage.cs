using System.Reflection;
using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Host;

public class PluginStorage
{
    public List<Type> CommandTypes { get; init; } = [];

    public void LoadPlugins(string pluginsDir)
    {
        if (!Directory.Exists(pluginsDir))
        {
            Console.WriteLine($"НЕ НАЙДЕНО: [{pluginsDir}]");
            return;
        }

        foreach (var filename in Directory.EnumerateFiles(pluginsDir, "*.dll"))
        {
            var lib = Assembly.LoadFile(filename);
            var target = typeof(ICommand);

            // все что комманды не асбтрактное и не интерфейс
            var commands = lib
                .GetExportedTypes()
                .Where(t => target.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            commands.ForEach(t => Console.WriteLine($"LOADED: [{t}]"));
            CommandTypes.AddRange(commands);
        }
    }
}

