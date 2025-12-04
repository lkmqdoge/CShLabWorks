using CShLabWorks.First;

namespace CShLabWorks.App;

public class App1 : IApp
{
    public static void Run()
    {
        string input;
        Console.Write("Введите ipv4 аддрес: ");
        input = Console.ReadLine() ?? "";

        try {
            Console.WriteLine($"Парсинг строки с исключениями {IPv4Address.Parse(input)}");
        }catch(Exception e){
            Console.WriteLine($"Строка не прошла парсинг с {e}");
            return;
        }

        if (IPv4Address.TryParse(input, out IPv4Address address))
            Console.WriteLine($"Строка прошла парсинг с TryParse {address}");
        else
            Console.WriteLine("Строка не прошла парсинг с TryParse");

        Console.WriteLine("Введите длинну маски для ipv4cidr");
        if (int.TryParse(Console.ReadLine(), out var l))
        {
            var a = IPv4Address.Parse(input);
            var c = new Ipv4Cidr(a, l);

            Console.WriteLine($"Маска:{c.Mask}, Сеть:{c.Network}");
            Console.WriteLine($"скллолько пользователей:{c.HostCount}");
            Console.WriteLine($"бродкаст?:{c.Broadcast}");
        }
        else {
            Console.WriteLine("неправильная длинна маски");
        }
    }
}
