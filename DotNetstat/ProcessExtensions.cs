using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

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
        if (OperatingSystem.IsLinux()) return process.GetChildProcessesOnLinux(dictionary);
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

        var psOutput = "ps -ef".Bash();
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

public static class ShellHelper
{
    //https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
    public static string Bash(this string cmd)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}