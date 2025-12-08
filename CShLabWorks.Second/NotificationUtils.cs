namespace CShLabWorks.Second;

public static class NotificationUtils
{
    // форматирует объект-оповещение в строку на основании его типа,
    // приоритета и свойств. Для каждого типа оповещения должен быть свой формат вывода
    //
    public static string Format(Notification notification)
    {
        if (notification is EmailNotification en)
            return $"{en.EmailAddress}\n\n{en.Subject}\n\n{en.Message}";
        else if (notification is SmsNotification sn)
            return $"{sn.PhoneNumber}\n\n{sn.Message}";
        else if (notification is PushNotification pn)
            return $"{pn.AppName}:{pn.DeviceId}\n\n{pn.Message}";
        else // notification is Norification
            return $"{notification.Title}\n\n{notification.Message}";
    }

    //  возвращает адрес получателя оповещения в зависимости от его тип
    public static string GetDestination(Notification notification)
    {
        if (notification is EmailNotification en)
            return en.EmailAddress;
        else if (notification is SmsNotification sn)
            return sn.PhoneNumber;
        else if (notification is PushNotification pn)
            return pn.DeviceId.ToString();
        else
            return "НЕ УКАЗАН";
    }
}

