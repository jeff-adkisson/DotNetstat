namespace DotNetstat.Models;

public class CommandModel
{
    public Platform Platform { get; init; } = Platform.Automatic;
    
    public string Shell { get; init; } = "";
    
    public string Name { get; init; } = "";

    public string Arguments { get; init; } = "";
    
    public ParsingModel Parsing { get; init; } = new();
}