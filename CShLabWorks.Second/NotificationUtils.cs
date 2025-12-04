namespace CShLabWorks.Second;

public static class NotificationUtils
{
    // форматирует объект-оповещение в строку на основании его типа,
    // приоритета и свойств. Для каждого типа оповещения должен быть свой формат вывода
    public static string Format(Notification notification)
    {
        throw new NotImplementedException();
    }

    public static string Format(EmailNotification emailNotification)
    {
        throw new NotImplementedException();
    }

    //  возвращает адрес получателя оповещения в зависимости от его тип
    public static string GetDestination(Notification notification)
    {
        throw new NotImplementedException();
    }
}

