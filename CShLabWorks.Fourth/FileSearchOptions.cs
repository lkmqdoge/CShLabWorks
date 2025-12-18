namespace CShLabWorks.Fourth;

public class FileSearchOptions
{
    public string Root { get; set; } = ".";

    // файлы в которых поиск 
    public string[] Masks { get; set; } = [];

    public string ToFind { get; set; } = string.Empty;

    public int MaxJobs { get; set; } = Environment.ProcessorCount;
}

