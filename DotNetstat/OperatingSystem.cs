using System.Runtime.InteropServices;

namespace DotNetstat;

internal static class OperatingSystem
{
    //from https://mariusschulz.com/blog/detecting-the-operating-system-in-net-core

    internal static bool IsWindows()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    internal static bool IsMacOSx()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    internal static bool IsLinux()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}