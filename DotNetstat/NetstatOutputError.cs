namespace DotNetstat;

public class NetstatOutputError : NetstatOutput
{
    public NetstatOutputError(string error, ICommand command, string originalOutput, IReadOnlyCollection<NetstatLine>? lines = null)
        : base(command, originalOutput, lines ?? new List<NetstatLine>().AsReadOnly())
    {
        Success = false;
        Error = error;
    }
}