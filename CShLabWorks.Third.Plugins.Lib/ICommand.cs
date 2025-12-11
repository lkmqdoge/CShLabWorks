namespace CShLabWorks.Third.Plugins.Lib;

public interface ICommand
{
    string Name { get; set; }

    string Description { get; set; }

    int Execute(params string[] args);
}

