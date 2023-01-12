using System.Diagnostics;

namespace DotNetstat.ProcessTree;

/// <summary>
///     https://www.derpturkey.com/c-process-tree/
/// </summary>
public sealed class Tree
{
    internal Tree(Process process, Dictionary<int, Process> processes, int depth = 0)
    {
        Process = process;
        ChildProcesses = new List<Tree>();

        var childProcesses = Process.GetChildren(processes);
        depth++;

        if (depth > 5)
            return;
        foreach (var childProcess in childProcesses)
            ChildProcesses.Add(new Tree(childProcess, processes, depth));
    }

    public Process Process { get; }

    public List<Tree> ChildProcesses { get; }

    public int Id => Process.Id;

    public string ProcessName => Process.ProcessName;

    public long Memory => Process.PrivateMemorySize64;
}