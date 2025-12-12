using System.Reflection;

namespace CShLabWorks.Third.Plugins.Host;

public class PluginStorage
{
    public void LoadPlugins(string pluginsDir)
    {
        foreach (var item in Directory.EnumerateFiles(pluginsDir))
        {
            if (Path.GetExtension(item) == ".dll")
            {
                var lib = Assembly.LoadFile(item);

                foreach(Type type in lib.GetExportedTypes())
                {
                    var c = Activator.CreateInstance(type);
                }
            }
        }
    }
}

