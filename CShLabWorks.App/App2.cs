namespace CShLabWorks.App;

using CShLabWorks.Second;

public class App2 : IApp
{
    public static void Run()
    {
        var notification = NotificationFactory.CreateEmailFactory(NotificationPriority.Low)
            ("sas@pis.me", "nothing");

        Console.WriteLine($"from email factory: {notification}");

        var t = NotificationFactory.CreateFromTemplate(
            "MEOW rrrr ZZZ sdklsadjdskaljdsalk",
            new () {
                {"title", "XUZ"},
                {"MEOW","meow"},
                {"rrrr", "RR"},
                {"ZZZ", "X"}
            }
        );
        Console.WriteLine($"from template: {t}");
        var c = new SmsNotification()
        {
            Message = "THIS IS CRITICAL",
            Title = "TITILE TEXT",
            PhoneNumber = "8-999-982-17-46",
            Priority = NotificationPriority.Critical
        };

        var nm = new NotificationManager();

        nm.Subsribe(NotificationPriority.Low, n =>
        {
            Console.WriteLine("subscribe-low:");
            Console.WriteLine(NotificationUtils.Format(n));
        });
        nm.CriticalNotificationSend += n =>
        {
            Console.WriteLine("event critical");
            Console.WriteLine(NotificationUtils.Format(n));
        };
        nm.NotificationFilter += n =>
        {
            Console.WriteLine("event filter");
            Console.WriteLine(NotificationUtils.Format(n));
        };


        Console.WriteLine("После отправления сообщения:");
        nm.Send(notification);

        Console.WriteLine("После отправления критического сообщения:");
        nm.Send(c);

        Console.WriteLine($"Сообщений в час: {nm.GetAverageNotificationsPerHour()}");
        Console.WriteLine($"В группе: ");
        foreach (var entry in nm.GroupByType())
        {
            Console.Write($"Key: {entry.Key}, Value: ");
            entry.Value.ForEach(Console.Write);
            Console.Write("\n");
        }

        Console.WriteLine("with linq");
        nm.NotificationHistory
            .Where(n => n.Priority is NotificationPriority.Low)
            .ToList()
            .ForEach(n => Console.WriteLine(NotificationUtils.GetDestination(n)));
    }
}
