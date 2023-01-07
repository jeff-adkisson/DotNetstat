using System.Diagnostics;

namespace DotNetstat;

/// <summary>
///     https://www.derpturkey.com/c-process-tree/
/// </summary>
public sealed class ProcessTree
{
    private ProcessTree(Process process, Dictionary<int, Process> processes, int depth = 0)
    {
        Process = process;
        ChildProcesses = new List<ProcessTree>();

        var childProcesses = Process.GetChildProcesses(processes);
        depth++;

        if (depth > 5)
            return;
        foreach (var childProcess in childProcesses)
            ChildProcesses.Add(new ProcessTree(childProcess, processes, depth));
    }

    public Process Process { get; }

    public List<ProcessTree> ChildProcesses { get; }

    public int Id => Process.Id;

    public string ProcessName => Process.ProcessName;

    public long Memory => Process.PrivateMemorySize64;

    public static ProcessTree Factory(Process parentProcess)
    {
        var processes = Processes.Running();
        return new ProcessTree(parentProcess, processes);
    }
}