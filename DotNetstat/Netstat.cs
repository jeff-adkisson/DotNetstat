using System.Diagnostics;

namespace DotNetstat;

public class Netstat
{
    public static IEnumerable<NetstatLine> Call(bool includeProcessDetails = true)
    {
        return Call(Platform.Automatic, includeProcessDetails);
    }

    public static IEnumerable<NetstatLine> Call(Flavor flavor, bool includeProcessDetails = true)
    {
        var cmd = flavor.Command();
        var output = ExecuteCommand(cmd);
        return ParserFactory.GetParser(flavor.RelatedPlatform(), includeProcessDetails).Parse(output);
    }

    public static IEnumerable<NetstatLine> Call(Platform platform, bool includeProcessDetails = true)
    {
        var cmd = platform.Command();
        var output = ExecuteCommand(cmd);
        var parser = ParserFactory.GetParser(platform, includeProcessDetails);
        return parser.Parse(output);
    }

    private static string ExecuteCommand(Command cmd)
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