using CShLabWorks.Third.Plugins.Lib;

namespace CShLabWorks.Third.Plugins.Math;

[CommandsAlias(Aliases = ["sum"])]
public class AddCommand : ICommand
{
    public string Name { get; set; } = "cat";

    public string Description { get; set; } =
        """
        add <число> <число> .. 

        Складывает все числа из аргументов
        """;

    public int Execute(params string[] args)
    {
        try
        {
            int result = args.Sum(int.Parse);
            Console.WriteLine(result);
        }
        catch
        {
            return -1;
        }
        return 0;
    }
}


