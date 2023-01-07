using System.Runtime.InteropServices;

namespace DotNetstat;

public static class PlatformAutoSelector
{
    public static Platform Select()
    {
        if (OperatingSystem.IsWindows()) return Platform.Windows;
        if (OperatingSystem.IsLinux()) return Platform.Linux;
        if (OperatingSystem.IsMacOSx()) return Platform.Osx;

        //default to windows since this is C#
        return Platform.Windows;
    }
}