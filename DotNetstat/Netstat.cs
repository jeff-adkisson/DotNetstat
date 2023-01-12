using System.Diagnostics;
using DotNetstat.NetstatParsing;

namespace DotNetstat;

public static class Netstat
{
    public static INetstatOutput Call(bool includeProcessDetails = true)
    {
        return Call(Platform.Automatic, includeProcessDetails);
    }

    public static INetstatOutput Call(ICommand command, bool includeProcessDetails = true)
    {
        var output = ExecuteCommand(command);
        return ParserFactory.Get(command.Platform, includeProcessDetails).Parse(output);
    }

    public static INetstatOutput Call(Platform platform, bool includeProcessDetails = true)
    {
        var cmd = platform.DefaultCommand();
        var output = ExecuteCommand(cmd);
        var parser = ParserFactory.Get(platform, includeProcessDetails);
        return parser.Parse(output);
    }

    private static string ExecuteCommand(ICommand cmd)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = cmd.Name,
                Arguments = cmd.Arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }
}