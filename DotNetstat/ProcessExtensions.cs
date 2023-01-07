using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;

namespace DotNetstat;

public static class ProcessExtensions
{
    public static ProcessTree GetProcessTree(this Process process)
    {
        return ProcessTree.Factory(process);
    }

    public static List<Process> GetChildProcesses(this Process process, Dictionary<int, Process> dictionary)
    {
#pragma warning disable CA1416
        if (OperatingSystem.IsWindows()) return process.GetChildProcessesOnWindows(dictionary);
#pragma warning restore CA1416

        return new List<Process>();
    }

    /// <summary>
    ///     Get the child processes for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    private static List<Process> GetChildProcessesOnWindows(this Process process, Dictionary<int, Process> dictionary)
    {
        var results = new List<Process>();

        if (!OperatingSystem.IsWindows()) return results;

        var queryText = $"select processid from win32_process where parentprocessid = {process.Id}";
        using var searcher = new ManagementObjectSearcher(queryText);
        foreach (var obj in searcher.Get())
        {
            var data = obj.Properties["processid"].Value;
            var childId = Convert.ToInt32(data);

            var childProcess = dictionary.ContainsKey(childId) ? dictionary[childId] : null;
            if (childProcess != null) results.Add(childProcess);
        }

        return results;
    }
}