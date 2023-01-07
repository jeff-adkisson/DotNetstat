using ConsoleTables;

namespace DotNetstat.Tests.Extensions;

public static class NetstatOutputExtensions
{
    public static string WriteLinesToTable(this INetstatOutput netstatOutput)
    {
        return ConsoleTable
            .From(netstatOutput.Lines.Select(line => new
                { line.Number, line.Protocol, line.LocalAddress, line.ForeignAddress, line.State, line.ProcessId }))
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .ToString();
    }

    public static string WriteLinesAndOriginalOutput(this INetstatOutput netstatOutput)
    {
        var table = netstatOutput.WriteLinesToTable();
        return $"{table}{Environment.NewLine}{Environment.NewLine}{netstatOutput.OriginalOutput}";
    }
}