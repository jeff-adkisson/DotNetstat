namespace DotNetstat;

public static class ParserFactory
{
    private static INetstatParser? _windowsParserInst;
    
    public static INetstatParser GetParser(Platform platform)
    {
        switch (platform)
        {
            case Platform.Windows:
                return _windowsParserInst = _windowsParserInst ?? new NetstatParserWindows();
            case Platform.Linux:
            case Platform.MacOs:
            default:
                throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
        }
    }
}