using ConsoleTables;

namespace DotNetstat.Tests.Extensions;

public static class NetstatOutputExtensions
{
    public static string WriteLinesToTable(this INetstatOutput netstatOutput) =>
        ConsoleTable
            .From(netstatOutput.Lines)
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .ToString();

    public static string WriteLinesAndOriginalOutput(this INetstatOutput netstatOutput)
    {
        var table = netstatOutput.WriteLinesToTable();
        return $"{table}{Environment.NewLine}{Environment.NewLine}{netstatOutput.OriginalOutput}";
    }
}