using System.Diagnostics;

namespace DotNetstat;

public static class ProcessTreeExtensions
{
    public static IEnumerable<Process> Flatten(this ProcessTree processTree)
    {
        var processes = new List<Process>();
        processTree.Flatten(processes);
        return processes;
    }
    
    private static void Flatten(this ProcessTree processTree, ICollection<Process> processes)
    {
        processes.Add(processTree.Process);
        foreach (var childProcess in processTree.ChildProcesses)
            childProcess.Flatten(processes);
    }
}