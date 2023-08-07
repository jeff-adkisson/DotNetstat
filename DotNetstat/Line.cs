using System.Diagnostics;
using DotNetstat.NetstatParsing;

namespace DotNetstat;

public sealed record Line : OriginalLine
{
    public Line(int lineNbr, string originalLine, Process? process) : base(lineNbr, originalLine)
    {
        Process = process;
        if (process == null) return;

        try
        {
            ModuleName = process.MainModule?.ModuleName ?? "";
        }
        catch (Exception)
        {
            ModuleName = "Not Available";
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Returned if process ID cannot be parsed
    /// </summary>
    public static int ProcessIdNotSpecified => -1;

    public string Protocol { get; init; } = "Unknown";

    public Address LocalAddress { get; init; } = new("default");

    public Address ForeignAddress { get; init; } = new("default");

    public string State { get; init; } = "Unknown";

    public int ProcessId { get; init; } = ProcessIdNotSpecified;

    public string ModuleName { get; init; } = "";

    public Process? Process { get; init; }

    public override string ToString()
    {
        return
            $"Proto {Protocol} | Local {LocalAddress} | Foreign {ForeignAddress} | State {State} | Process {ProcessId}";
    }
}