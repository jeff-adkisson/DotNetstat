namespace DotNetstat;

internal static class ParserFactory
{
    internal static Parser Get(Platform platform, bool includeProcessDetails)
    {
        return Get(platform.DefaultCommand(), includeProcessDetails);
    }

    private static Parser Get(ICommand command, bool includeProcessDetails)
    {
        return new Parser(command, includeProcessDetails);
    }
}