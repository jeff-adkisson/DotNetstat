using System.Runtime.InteropServices;

namespace DotNetstat;

internal static class PlatformDetector
{
    internal static Platform Detect()
    {
        if (IsWindows()) return Platform.Windows;
        if (IsLinux()) return Platform.Linux;
        if (IsMacOSx()) return Platform.Osx;

        //default to windows if indeterminate since this is C# and that's a decent bet
        return Platform.Windows;
    }

    internal static ICommand GetCommand() => Detect().GetCommand();

    private static bool IsWindows()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    private static bool IsMacOSx()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    private static bool IsLinux()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}