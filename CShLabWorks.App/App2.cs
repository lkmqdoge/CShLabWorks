namespace CShLabWorks.App;

using CShLabWorks.Second;

public class App2 : IApp
{
    public static void Run()
    {
        var n = NotificationFactory.CreateEmailFactory(NotificationPriority.Low)("xuz@pis.me", "nothing");
        Console.WriteLine(n);
    }
}
