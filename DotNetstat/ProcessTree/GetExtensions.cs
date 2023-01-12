using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using DotNetstat.Shell;

namespace DotNetstat.ProcessTree;

public static class GetExtensions
{
    private static readonly ICommand LinuxShellCommand = new Command(Platform.Linux, "ps -ef");

    public static Tree GetTree(this Process parentProcess)
    {
        var processes = Processes.Running();
        return new Tree(parentProcess, processes);
    }

    public static List<Process> GetChildren(this Process process)
    {
        return process.GetChildren(Processes.Running());
    }

    internal static List<Process> GetChildren(this Process process, Dictionary<int, Process> dictionary)
    {
#pragma warning disable CA1416
        if (OperatingSystem.IsWindows()) return process.GetChildProcessesOnWindows(dictionary);
        if (OperatingSystem.IsLinux()) return process.GetChildProcessesOnLinux(dictionary);
        if (OperatingSystem.IsMacOSx()) throw new PlatformNotSupportedException("GetChildProcesses does not support MacOS yet");
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

    /// <summary>
    ///     Get the child processes for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    private static List<Process> GetChildProcessesOnLinux(this Process process, Dictionary<int, Process> dictionary)
    {
        var results = new List<Process>();

        if (!OperatingSystem.IsLinux()) return results;

        var psOutput = Platform.Linux.ExecuteShellCommand(LinuxShellCommand);
        var regex = new Regex(@"^\s*(?:\S+)\s+(?<pid>\d+)\s+(?<ppid>\d+).*$", RegexOptions.Multiline);
        var matches = regex.Matches(psOutput);

        foreach (Match match in matches)
        {
            var pid = int.Parse(match.Groups["pid"].Value);
            var ppid = int.Parse(match.Groups["ppid"].Value);
            if (ppid != process.Id) continue;

            var childProcess = dictionary.ContainsKey(pid) ? dictionary[pid] : null;
            if (childProcess != null) results.Add(childProcess);
        }

        return results;
    }
}