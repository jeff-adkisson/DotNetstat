namespace DotNetstat;

internal static class ParserFactory
{
    private static INetstatParser? _windowsParserInst;

    public static INetstatParser GetParser(Platform platform)
    {
        while (true)
            switch (platform)
            {
                case Platform.Automatic:
                    var selectedPlatform = PlatformAutoSelector.Select();
                    platform = selectedPlatform;
                    continue;

                case Platform.Windows:
                    return _windowsParserInst = _windowsParserInst ?? new NetstatParserWindows();

                case Platform.Linux:
                case Platform.Osx:
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }
    }
}