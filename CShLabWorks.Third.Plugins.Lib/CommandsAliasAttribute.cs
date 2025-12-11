namespace CShLabWorks.Third.Plugins.Lib;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CommandsAliasAttribute(params string[] aliases)
    : Attribute
{
    public string[] Aliases = aliases;
}

