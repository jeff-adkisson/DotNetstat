using ConsoleTables;

namespace DotNetstat.Tests.Extensions;

public static class NetstatOutputExtensions
{
    public static string WriteLinesToTable(this IOutput output)
    {
        return ConsoleTable
            .From(output.Lines.Select(line => new
                { line.Number, line.Protocol, line.LocalAddress, line.ForeignAddress, line.State, line.ProcessId }))
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .ToString();
    }

    public static string WriteLinesAndOriginalOutput(this IOutput output)
    {
        var table = output.WriteLinesToTable();
        return $"{table}{Environment.NewLine}{Environment.NewLine}{output.OriginalOutput}";
    }
}