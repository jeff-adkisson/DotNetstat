using System.Diagnostics;

namespace DotNetstat;

public class NetStat
{
    public static IEnumerable<NetstatLine> Call() => Call(Platform.Automatic);
    
    public static IEnumerable<NetstatLine> Call(Flavor flavor)
    {
        var cmd = flavor.Command();
        var output = ExecuteCommand(cmd);
        return ParserFactory.GetParser(flavor.RelatedPlatform()).Parse(output);
    }

    public static IEnumerable<NetstatLine> Call(Platform platform)
    {
        var cmd = platform.Command();
        var output = ExecuteCommand(cmd);
        return ParserFactory.GetParser(platform).Parse(output);
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