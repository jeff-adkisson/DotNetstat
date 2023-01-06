namespace DotNetstat;

public sealed record NetstatLine
{
    public string Protocol { get; init; } = "";

    public string LocalAddress { get; init; } = "";

    public string ForeignAddress { get; init; } = "";

    public string State { get; init; } = "";

    public int ProcessId { get; set; }
}