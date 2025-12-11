namespace CShLabWorks.Third.Plugins.Host;

public class PluginStorage
{
    void LoadPlugins(string pluginsDir)
    {
        foreach (var item in Directory.EnumerateFiles(pluginsDir))
        {
            if (Path.GetExtension(item) == ".dll")
            {
            
            }
        }
    }
}

