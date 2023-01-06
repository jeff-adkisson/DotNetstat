using System.Diagnostics;

namespace DotNetstat;

public static class Processes
{
    public static Dictionary<int, Process> GetRunningProcesses()
    {
        return Process
            .GetProcesses()
            .ToDictionary(key => key.Id, val => val);
    }
}