using System.Diagnostics;

namespace DotNetstat;

public static class Processes
{
    public static Dictionary<int, Process> Running()
    {
        return Process
            .GetProcesses()
            .ToDictionary(key => key.Id, val => val);
    }

    public static Process? ByProcessId(int processId)
    {
        try
        {
            return Process.GetProcessById(processId);
        }
        catch (Exception)
        {
            return null;
        }
    }
}