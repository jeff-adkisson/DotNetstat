using System.Diagnostics;

namespace DotNetstat.ProcessTree;

public static class FlattenProcessTreeExtensions
{
    public static IEnumerable<Process> Flatten(this Tree processTree)
    {
        var processes = new List<Process>();
        processTree.Flatten(processes);
        return processes;
    }

    private static void Flatten(this Tree processTree, ICollection<Process> processes)
    {
        processes.Add(processTree.Process);
        foreach (var childProcess in processTree.ChildProcesses)
            childProcess.Flatten(processes);
    }
}