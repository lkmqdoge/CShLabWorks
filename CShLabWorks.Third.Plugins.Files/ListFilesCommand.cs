using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Files;

[CommandsAlias(Aliases = ["ls", "ll", "l"])]
public class ListFilesCommand : ICommand
{
    public string Name { get; set; } = "listfiles";

    public string Description { get; set; } =
        """
        listfiles <ПУТЬ/К/ДИРЕКТОРИИ>
        Выводит содержимое указаноой директории
        """;

    // пишет все файлы/директории в консоль (как ll)
    public int Execute(params string[] args)
    {
        try
        {
            var dir = args.Length == 0 ? "." : args[0];
            var path = Path.Combine(Environment.CurrentDirectory, dir);
            var l = Directory.EnumerateFileSystemEntries(path);
            l.ToList().ForEach(Console.WriteLine);
        }
        catch
        {
            return -1;
        }

        return 0;
    }
}


