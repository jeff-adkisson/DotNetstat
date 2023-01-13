using System.Diagnostics;

namespace DotNetstat.Shell;

internal static class Execute
{
    internal static string Command(string shell, string command, string? arguments = null)
    {
        arguments = arguments ?? string.Empty;
        var cmd = $"{command} {arguments}";
        var escapedArgs = cmd.Replace("\"", "\\\"");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = shell,
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}