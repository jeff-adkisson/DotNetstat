using System.Runtime.InteropServices;

namespace DotNetstat;

public static class OperatingSystem
{
    //from https://mariusschulz.com/blog/detecting-the-operating-system-in-net-core

    public static bool IsWindows()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public static bool IsMacOSx()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    public static bool IsLinux()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}