namespace DotNetstat;

public sealed record NetstatRecord
{
    public string Protocol { get; init; } = "";

    public string LocalAddress { get; init; } = "";
    
    public string ForeignAddress { get; set; }
    
    public string State { get; set; }
    
    public int ProcessId { get; set; }
}