using DotNetstat.ProcessTree;

namespace DotNetstat;

public static class NetstatExtensions
{
    /// <summary>
    ///     Returns all netstat output related to a specific process ID.
    ///     Returns empty collection if no matching process is found.
    /// </summary>
    /// <param name="netstatOutput"></param>
    /// <param name="processId"></param>
    /// <returns></returns>
    public static IEnumerable<Line> ByProcessId(
        this IOutput netstatOutput,
        int processId)
    {
        var result = new List<Line>();

        var enumerable = netstatOutput.Lines.ToArray();

        var currentProcess = Processes.GetProcessById(processId);
        if (currentProcess == null) return result;

        var processTree = currentProcess.GetTree();
        var allProcesses = processTree.Flatten();
        foreach (var process in allProcesses) result.AddRange(enumerable.Where(n => n.ProcessId == process.Id));
        return result.DistinctBy(r => r.LocalAddress.Port);
    }

    /// <summary>
    ///     Returns all netstat output related to a specific local port.
    ///     Returns empty collection if no matching local port is found.
    /// </summary>
    /// <param name="netstatOutput"></param>
    /// <param name="localPort"></param>
    /// <returns></returns>
    public static IEnumerable<Line> ByLocalPort(
        this IOutput netstatOutput,
        int localPort)
    {
        return netstatOutput.Lines.Where(n => n.LocalAddress.Port == localPort).ToList();
    }

    /// <summary>
    ///     Returns all netstat output related to a specific foreign port.
    ///     Returns empty collection if no matching foreign port is found.
    /// </summary>
    /// <param name="netstatLines"></param>
    /// <param name="foreignPort"></param>
    /// <returns></returns>
    public static IEnumerable<Line> ByForeignPort(
        this IEnumerable<Line> netstatLines,
        int foreignPort)
    {
        return netstatLines.Where(n => n.ForeignAddress.Port == foreignPort).ToList();
    }
}