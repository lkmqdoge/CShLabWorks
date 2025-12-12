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

var ch = new CommandHost(path);
ch.Run();

