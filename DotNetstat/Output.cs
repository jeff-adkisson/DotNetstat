using System.Text;
using DotNetstat.NetstatParsing;

namespace DotNetstat;

public class Output : IOutput
{
    public Output(ICommand command, IEnumerable<Line> lines, IEnumerable<OriginalLine> unparsedLines)
    {
        Lines = lines.ToList().AsReadOnly();
        UnparsedLines = unparsedLines.ToList().AsReadOnly();
        Command = command;
        Success = true;
    }

    public Platform Platform { get; init; }

    public IReadOnlyCollection<Line> Lines { get; init; }

    public IReadOnlyCollection<OriginalLine> UnparsedLines { get; init; }

    public ICommand Command { get; init; }

    public bool Success { get; init; }

    public string Error { get; init; } = "";

    public string OriginalOutput
    {
        get
        {
            var sb = new StringBuilder();
            var allLines = Lines
                .Concat(UnparsedLines)
                .OrderBy(x => x.Number)
                .ToList();

            var spacing = Math.Floor(Math.Log(allLines.Count, 10)) + 1;
            for (var i = 0; i < allLines.Count(); i++)
            {
                var number = string.Format($"{{0,{spacing}}}", i + 1);
                sb.AppendLine($"{number} | {allLines[i].Data}");
            }

            return sb.ToString();
        }
    }
}