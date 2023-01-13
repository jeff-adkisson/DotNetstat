namespace DotNetstat.NetstatParsing;

public class OutputError : Output
{
    public OutputError(string error, ICommand command, IReadOnlyCollection<Line>? lines = null,
        IReadOnlyCollection<Line>? unparsedLines = null)
        : base(command, lines ?? new List<Line>(), unparsedLines ?? new List<Line>())
    {
        Success = false;
        Error = error;
    }
}