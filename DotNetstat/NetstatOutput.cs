namespace DotNetstat;

public class NetstatOutput : INetstatOutput
{
    public NetstatOutput(ICommand command, string originalOutput, IReadOnlyCollection<NetstatLine> lines)
    {
        Lines = lines;
        OriginalOutput = originalOutput;
        Command = command;
        Success = true;
        
    }

    public IReadOnlyCollection<NetstatLine> Lines { get; init; }
    
    public string OriginalOutput { get; init; }

    public ICommand Command { get; init; }

    public Platform Platform { get; init; }

    public bool Success { get; init; }

    public string Error { get; init; } = "";
}