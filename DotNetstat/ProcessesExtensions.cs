using System.Diagnostics;

namespace DotNetstat;

public static class ProcessesExtensions
{
    public static Process? ByProcessId(this Dictionary<int, Process> processes, int processId)
    {
        return processes.TryGetValue(processId, out var value) ? value : null;
    }
}