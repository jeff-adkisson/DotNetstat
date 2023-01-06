namespace DotNetstat;

internal static class ParserFactory
{
    private static INetstatParser? _windowsParserNoProcessDetailsInst;

    private static INetstatParser? _windowsParserWithProcessDetailsInst;

    public static INetstatParser GetParser(Platform platform, bool includeProcessDetails = true)
    {
        while (true)
            switch (platform)
            {
                case Platform.Automatic:
                    var selectedPlatform = PlatformAutoSelector.Select();
                    platform = selectedPlatform;
                    continue;

                case Platform.Windows:
                    if (includeProcessDetails)
                        return _windowsParserWithProcessDetailsInst 
                            ??= new NetstatParserWindows(true);
                    
                    return _windowsParserNoProcessDetailsInst 
                        ??= new NetstatParserWindows(false);

                case Platform.Linux:
                case Platform.Osx:
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }
    }
}