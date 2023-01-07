using System.Text;

namespace DotNetstat;

public class NetstatOutput : INetstatOutput
{
    public NetstatOutput(ICommand command, IEnumerable<NetstatLine> lines, IEnumerable<Line> unparsedLines)
    {
        Lines = lines.ToList().AsReadOnly();
        UnparsedLines = unparsedLines.ToList().AsReadOnly();
        Command = command;
        Success = true;
    }

    public Platform Platform { get; init; }

    public IReadOnlyCollection<NetstatLine> Lines { get; init; }

    public IReadOnlyCollection<Line> UnparsedLines { get; init; }

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
                sb.AppendLine($"{number} | {allLines[i].OriginalLine}");
            }

            return sb.ToString();
        }
    }
}