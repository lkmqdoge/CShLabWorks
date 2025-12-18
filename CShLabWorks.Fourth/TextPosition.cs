namespace CShLabWorks.Fourth;

public readonly struct TextPosition(string from, int col, int row)
{
    public readonly string FromFile
        => from;

    public readonly int Column
        => col;

    public readonly int Row
        => row;

    public override string ToString()
        => $"{row}:{col} from {FromFile}";
}

