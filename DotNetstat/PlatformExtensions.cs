namespace DotNetstat;

public static class PlatformExtensions
{
    public static ICommand DefaultCommand(this Platform platform)
    {
        return Commands.DefaultByPlatform(platform);
    }
}