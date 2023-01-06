using System.Runtime.InteropServices;

namespace DotNetstat;

public static class PlatformAutoSelector
{
    public static Platform Select()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return Platform.Windows;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return Platform.Linux;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return Platform.Osx;

        //default to windows since this is C#
        return Platform.Windows;
    }
}