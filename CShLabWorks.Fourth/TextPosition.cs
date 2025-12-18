namespace CShLabWorks.Fourth;

public readonly struct TextPosition(string from, int col, int row)
{
    public readonly string FromFile { get; } = from;

    public readonly int Column { get; } = col;

    public readonly int Row { get;} = row;

    public override string ToString()
        => $"{row}:{col} from {FromFile}";
}

