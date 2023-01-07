namespace DotNetstat;

internal static class ParserFactory
{
    public static Parser Get(Platform platform, bool includeProcessDetails)
    {
        return Get(platform.DefaultCommand(), includeProcessDetails);
    }

    public static Parser Get(ICommand command, bool includeProcessDetails)
    {
        return new(command, includeProcessDetails);
    }
}