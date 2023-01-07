using System.Diagnostics;

namespace DotNetstat;

public sealed record NetstatLine(Process? Process)
{
    private int? _foreignPort;
    private int? _localPort;

    public string Protocol { get; init; } = "Unknown";

    public string LocalAddress { get; init; } = "Unknown";

    public string ForeignAddress { get; init; } = "Unknown";

    public string State { get; init; } = "Unknown";

    public int ProcessId { get; init; } = PortNotSpecified;

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Returned if address does not have a port or the port cannot be parsed
    /// </summary>
    public static int PortNotSpecified => -1;

    public int LocalPort =>
        _localPort ?? (_localPort = ParsePortFromAddress(LocalAddress)).Value;

    public int ForeignPort =>
        _foreignPort ?? (_foreignPort = ParsePortFromAddress(ForeignAddress)).Value;

    private static int ParsePortFromAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address)) return PortNotSpecified;

        var parts = address.Split(':');
        if (parts.Length != 2) return PortNotSpecified;
        var parsed = int.TryParse(parts[1], out var port);
        return parsed ? port : PortNotSpecified;
    }
    
    public override string ToString()
    {
        return $"Proto {Protocol} | Local {LocalAddress}, {LocalPort} | Foreign {ForeignAddress} {ForeignPort} | State {State} | Process {ProcessId}";
    }
}