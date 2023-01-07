namespace DotNetstat;

public record Line
{
    public Line(int lineNbr, string originalLine)
    {
        Number = lineNbr;
        OriginalLine = (originalLine ?? "").TrimEnd('\r', '\n');
        ;
    }

    public int Number { get; }

    public string OriginalLine { get; }
}