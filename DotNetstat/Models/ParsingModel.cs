namespace DotNetstat.Models;

public class ParsingModel
{
    public string NetstatParserRegex { get; init; } = "";

    public string ProcessIdParserRegex { get; init; } = "";

    public string GetProcessesCommand { get; init; } = "";

    public string GetProcessesParserRegex { get; init; } = ".*";

    public string ParseAddressAndPortRegex { get; init; } = "";
}