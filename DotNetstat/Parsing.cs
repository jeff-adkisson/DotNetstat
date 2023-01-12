using System.Text.RegularExpressions;
using DotNetstat.Models;

namespace DotNetstat;

public sealed class Parsing
{
    public Parsing(ParsingModel model)
    {
        var regex = string.IsNullOrWhiteSpace(model.NetstatParserRegex) ? ".*" : model.NetstatParserRegex;
        NetstatParser = new Regex(regex, RegexOptions.Compiled);
        var regexProcessId = string.IsNullOrWhiteSpace(model.ProcessIdParserRegex) ? ".*" : model.ProcessIdParserRegex;
        ProcessIdParser = new Regex(regexProcessId, RegexOptions.Compiled);
    }

    public Regex NetstatParser { get; }

    public Regex ProcessIdParser { get; }

    public bool IsProcessIdParsingEnabled => ProcessIdParser.ToString() != ".*";
}