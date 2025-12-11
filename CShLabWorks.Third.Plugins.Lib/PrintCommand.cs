namespace CShLabWorks.Third.Plugins.Lib;

[CommandsAlias(Aliases = ["write", "echo"])]
public class PrintCommand : ICommand
{
    public string Name { get; set; } = "print";

    public string Description { get; set; } =
        """
        print <СООБЩЕНИЕ>

        Выводит в консоль сообщение переданное в аргументы. 
        для передачи сообщения, разделенного пробелами, заключите сообщение в ковычки
        """;

    public int Execute(params string[] args)
    {
        var message = args.Length == 1 ? args[0] : "";
        Console.WriteLine(message);
        return 0;
    }
}

