using System.Diagnostics;
using DotNetstat.NetstatParsing;

namespace DotNetstat;

public static class Netstat
{
    /// <summary>
    ///     Returns netstat output. Choice of platform is automatically selected.
    /// </summary>
    /// <param name="includeProcessDetails"></param>
    /// <returns></returns>
    public static IOutput Call(bool includeProcessDetails = true)
    {
        return Call(Platform.Automatic, includeProcessDetails);
    }

    /// <summary>
    ///     Returns netstat output using the supplied <see cref="ICommand" />.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="includeProcessDetails"></param>
    /// <returns></returns>
    public static IOutput Call(ICommand command, bool includeProcessDetails = true)
    {
        var output = ExecuteCommand(command);
        return ParserFactory.Get(command.Platform, includeProcessDetails).Parse(output);
    }

    /// <summary>
    ///     Returns netstat output using the supplied <see cref="Platform" />.
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="includeProcessDetails"></param>
    /// <returns></returns>
    public static IOutput Call(Platform platform, bool includeProcessDetails = true)
    {
        var output = ExecuteCommand(platform.GetCommand());
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