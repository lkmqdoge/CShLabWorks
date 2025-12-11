namespace CShLabWorks.Third.Plugins.Lib;

[CommandsAlias(Aliases = ["bat", "content", "printfile"])]
public class CatCommand : ICommand
{
    public string Name { get; set; } = "cat";

    public string Description { get; set; } =
        """
        cat <ПУТЬ/К/ФАЙЛУ>

        Печатает в консоль содержимое файла
        """;

    public int Execute(params string[] args)
    {
        if (args.Length == 0)
            return -1;

        try
        {
            var filename = args[0];
            var path = Path.Combine(Environment.CurrentDirectory, filename);
            var content = File.ReadAllText(path);
            Console.WriteLine(content);
        }
        catch
        {
            return -2;
        }
        return 0;
    }
}

