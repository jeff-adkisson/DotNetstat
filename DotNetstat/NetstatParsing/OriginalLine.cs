namespace DotNetstat.NetstatParsing;

public record OriginalLine
{
    public OriginalLine(int lineNbr, string originalLine)
    {
        Number = lineNbr;
        Data = (originalLine ?? "").TrimEnd('\r', '\n');
        ;
    }

    public int Number { get; }

    public string Data { get; }
}