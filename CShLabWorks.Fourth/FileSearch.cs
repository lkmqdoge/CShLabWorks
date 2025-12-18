using System.Collections.Concurrent;

namespace CShLabWorks.Fourth;

public static class FileSearch
{
    public static void Run()
    {
        // ввод директори
        Console.WriteLine("ВВОД <СЛОВО> <ПУТЬ/К/ДИРЕКТОРИИ> <\"*.txt\"> <\"*.log\"> ..");
        var r = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(r))
            return;
        var parts = r.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var options = new FileSearchOptions()
        {
            ToFind = parts[0],
            Root = Path.Combine(Environment.CurrentDirectory, parts[1]),
            Masks = [.. parts.Skip(2) ]
        };

        // паралелизм или не
        Console.WriteLine("Parallel? <yes/no> <MAXJOBS> (default=no)");
        var p = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(p))
            return;
        parts = p.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var useParralel = parts[0].Equals("yes", StringComparison.OrdinalIgnoreCase);

        var files = GetFiles(options);
        if (files is null)
        {
            Console.WriteLine("Файлы не напйдены");
            return;
        }

        var res = new List<TextPosition>();
        if (useParralel)
        {
            if (parts.Length == 2 && int.TryParse(parts[1], out var maxJobs))
                options.MaxJobs = maxJobs;

            var tokenSource = new CancellationTokenSource();
            Task.Run(() => {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    tokenSource.Cancel();
                }
            });
            for (int i = options.MaxJobs; i < Environment.ProcessorCount
                    && !tokenSource.IsCancellationRequested; i++)
            {
                options.MaxJobs = i;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                res = TextSearchParallel(files, options, tokenSource.Token);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine($"Сделано с {i} потоками за {elapsedMs} MC");
            }
        }
        else
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            res = TextSearch(files, options);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Сделано с 1 потокам за {elapsedMs} MC");
        }
    }

    public static string[] GetFiles(FileSearchOptions options)
    {
        var files = options.Masks
            .SelectMany(m => Directory.EnumerateFiles(options.Root, m))
            .Distinct();

        Console.WriteLine($"Найдено файлов {files.ToList().Count}");
        return [.. files ];
    }

    public static List<TextPosition> TextSearch(string[] files, FileSearchOptions options)
    {
        var res = new List<TextPosition>();
        foreach (var filename in files)
            SearchInFile(filename, options.ToFind, res);
        return res;
    }

    public static List<TextPosition> TextSearchParallel(
            string[] files,
            FileSearchOptions options,
            CancellationToken token)
    {
        var jobs = Math.Min(options.MaxJobs, files.Length);
        var q = new ConcurrentQueue<string>(files);
        var l = new List<Task>();
        var res = new ConcurrentBag<TextPosition>();

        for (int i = 0; i < jobs; i++)
        {
            l.Add(Task.Run(() => {
            while (q.TryDequeue(out var filename))
                SearchInFile(filename, options.ToFind, res);
            }, token));
        }

        try
        {
            Task.WaitAll(l, CancellationToken.None);
        }
        catch (AggregateException) {}

        return [.. res];
    }

    private static void SearchInFile(string filename, string pattern, List<TextPosition> results)
    {
        var lines = File.ReadAllLines(filename);
        for (int i = 0; i<lines.Length; i++) // строка
        {
            var s = lines[i];
            int index = s.IndexOf(pattern); // индедс в строке 

            while (index != -1)
            {
                var old = index;
                index = s.IndexOf(pattern, index + 1);
                results.Add(new TextPosition(filename, i, old));
            }
        }
    }

    private static void SearchInFile(string filename, string pattern, ConcurrentBag<TextPosition> results)
    {
        var lines = File.ReadAllLines(filename);
        for (int i = 0; i<lines.Length; i++) // строка
        {
            var s = lines[i];
            int index = s.IndexOf(pattern); // индедс в строке 

            while (index != -1)
            {
                var old = index;
                index = s.IndexOf(pattern, index + 1);
                results.Add(new TextPosition(filename, i, old));
            }
        }
    }
}

