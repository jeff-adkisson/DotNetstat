namespace DotNetstat;

public class NetstatOutputError : NetstatOutput
{
    public NetstatOutputError(string error, ICommand command, IReadOnlyCollection<NetstatLine>? lines = null, IReadOnlyCollection<NetstatLine>? unparsedLines = null)
        : base(command, lines ?? new List<NetstatLine>(), unparsedLines ?? new List<NetstatLine>())
    {
        Success = false;
        Error = error;
    }
}