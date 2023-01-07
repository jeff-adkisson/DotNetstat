namespace DotNetstat;

public static class NetstatExtensions
{
    public static IEnumerable<NetstatLine> ByProcessId(
        this IEnumerable<NetstatLine> netstatLines,
        int processId)
    {
        var result = new List<NetstatLine>();

        var enumerable = netstatLines as NetstatLine[] ?? netstatLines.ToArray();
        
        var currentProcess = Processes.Running().ByProcessId(processId);
        if (currentProcess == null) return result;
        
        var processTree = currentProcess.GetProcessTree();
        var allProcesses = processTree.Flatten();
        foreach (var process in allProcesses)
        {
            result.AddRange(enumerable.Where(n => n.ProcessId == process.Id));
        }
        return result.DistinctBy(r => r.LocalPort);
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