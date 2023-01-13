using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using DotNetstat.Shell;

namespace DotNetstat.ProcessTree;

public static partial class GetExtensions
{
    public static Tree GetTree(this Process parentProcess)
    {
        return new Tree(parentProcess, Processes.GetRunning());
    }

    public static List<Process> GetChildProcesses(this Process process)
    {
        return process.GetChildProcesses(Processes.GetRunning());
    }

    internal static List<Process> GetChildProcesses(this Process process, Processes processes)
    {
#pragma warning disable CA1416
        return OperatingSystem.IsWindows()
            ? process.GetChildProcessesOnWindows(processes)
            : process.GetChildProcessesFromShell(processes);
#pragma warning restore CA1416
    }

    /// <summary>
    ///     Get the child processes for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    private static List<Process> GetChildProcessesOnWindows(this Process process, Processes processes)
    {
        var results = new List<Process>();

        if (!OperatingSystem.IsWindows()) return results;

        var queryText = $"select processid from win32_process where parentprocessid = {process.Id}";
        using var searcher = new ManagementObjectSearcher(queryText);
        foreach (var obj in searcher.Get())
        {
            var data = obj.Properties["processid"].Value;
            var childId = Convert.ToInt32(data);

            var childProcess = processes.ContainsKey(childId) ? processes[childId] : null;
            if (childProcess != null) results.Add(childProcess);
        }

        return results;
    }

    /// <summary>
    ///     Get the child processes for a given process
    /// </summary>
    /// <param name="process"></param>
    /// <param name="processes"></param>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    private static List<Process> GetChildProcessesFromShell(this Process process, Processes processes)
    {
        var results = new List<Process>();

        if (!OperatingSystem.IsLinux()) return results;

        var cmd = PlatformDetector.GetCommand();
        var psOutput = Execute.Command(cmd.Shell, cmd.Parsing.GetProcessesCommand);
        var matches = cmd.Parsing.GetProcessesParser.Matches(psOutput);

        foreach (Match match in matches)
        {
            var pid = int.Parse(match.Groups["pid"].Value);
            var ppid = int.Parse(match.Groups["ppid"].Value);
            if (ppid != process.Id) continue;

            var childProcess = processes.ContainsKey(pid) ? processes[pid] : null;
            if (childProcess != null) results.Add(childProcess);
        }

        return results;
    }

    [GeneratedRegex("^\\s*(?:\\S+)\\s+(?<pid>\\d+)\\s+(?<ppid>\\d+).*$", RegexOptions.Multiline)]
    private static partial Regex LinuxPsCommandParserRegex();
}