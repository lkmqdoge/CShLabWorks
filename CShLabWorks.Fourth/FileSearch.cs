namespace CShLabWorks.Fourth;

public class FileSearch
{
    private bool _isRunning = true;

    public List<TextPosition> TextSearch(FileSearchOptions options)
    {
        var res = new List<TextPosition>();
        var files = options.Masks.SelectMany(m => Directory.EnumerateFiles(options.Root, m));
        foreach (var filename in files)
        {
            var lines = File.ReadAllLines(filename);
            // индекс строки
            for (int i = 0; i<lines.Length; i++)
            {
                var s = lines[i];
                int index = s.IndexOf(options.ToFind); // индедс в строке 

                while (index != -1)
                {
                    var old = index;
                    index = s.IndexOf(options.ToFind, index + 1);
                    res.Add(new TextPosition(filename, i, old));
                }
            }
        }
        return res;
    }

    // public List<TextPosition> ParallelTextSearch(FileSearchOptions options)
    // {
    //     var files = options.Masks.SelectMany(m => Directory.EnumerateFiles(options.Root, m));
    //     foreach (var filename in files)
    //     {
    //         var lines = File.ReadAllLines(filename);
    //     }
    // }

    public void Run()
    {
        var s = "sezon";
        var res = TextSearch(new FileSearchOptions()
        {
            ToFind=s,
            Root = "./trash",
            Masks = [ "*.txt", "*.names"]
        });
        foreach(var r in res)
            Console.WriteLine(r);


        // main loop
        while (_isRunning)
        {
            Console.WriteLine("ВВОД <ПУТЬ/К/ДИРЕКТОРИИ> <\".txt\"> <\".log\"> ..");
            var r = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(r))
                continue;

            var parts = r.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var options = new FileSearchOptions()
            {
                Root = parts[0],
                Masks = [.. parts.Skip(1) ]
            };

            if (string.IsNullOrWhiteSpace(r))
                continue;

            Console.WriteLine("Parallel? Y/yes N/no (default=no)");
            var p = Console.ReadLine();

            // часики 
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var useParralel = false;

            if (useParralel)
                continue;
            else
                TextSearch(options);

            // после завершения функц
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"ПРОШЛО ВРЕМЕНИ: {elapsedMs} МИЛИСКУНД");
        }
    }

    private static List<int> FindAllOccurrences(string text, string searchText)
    {
        var positions = new List<int>();
        int index = text.IndexOf(searchText);

        while (index != -1)
        {
            positions.Add(index);
            index = text.IndexOf(searchText, index + 1);
        }

        return positions;
    }
}

