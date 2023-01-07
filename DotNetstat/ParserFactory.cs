namespace DotNetstat;

internal static class ParserFactory
{
    public static Parser Get(Platform platform, bool includeProcessDetails) => 
        Get(platform.DefaultCommand(), includeProcessDetails);

    public static Parser Get(ICommand command, bool includeProcessDetails) => 
        new Parser(command, includeProcessDetails);
}