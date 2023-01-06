namespace DotNetstat;

public static class NetstatExtensions
{
    public static IEnumerable<NetstatLine> ByProcessId(
        this IEnumerable<NetstatLine> netstatLines,
        int processId)
    {
        return netstatLines.Where(n => n.ProcessId == processId).ToList();
    }

    public static IEnumerable<NetstatLine> ByLocalPort(
        this IEnumerable<NetstatLine> netstatLines,
        int localPort)
    {
        return netstatLines.Where(n => n.LocalPort == localPort).ToList();
    }

    public static IEnumerable<NetstatLine> ByForeignPort(
        this IEnumerable<NetstatLine> netstatLines,
        int foreignPort)
    {
        return netstatLines.Where(n => n.ForeignPort == foreignPort).ToList();
    }
}