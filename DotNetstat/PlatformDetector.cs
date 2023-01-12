namespace DotNetstat;

internal static class PlatformDetector
{
    internal static Platform Detect()
    {
        if (OperatingSystem.IsWindows()) return Platform.Windows;
        if (OperatingSystem.IsLinux()) return Platform.Linux;
        if (OperatingSystem.IsMacOSx()) return Platform.Osx;

        //default to windows since this is C#
        return Platform.Windows;
    }
}