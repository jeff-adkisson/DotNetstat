namespace DotNetstat.NetstatParsing;

internal static class ParserFactory
{
    internal static Parser Get(Platform platform, bool includeProcessDetails)
    {
        return Get(platform.GetCommand(), includeProcessDetails);
    }

    private static Parser Get(ICommand command, bool includeProcessDetails)
    {
        return new Parser(command, includeProcessDetails);
    }
}