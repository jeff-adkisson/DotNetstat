using System.Runtime.InteropServices;

namespace DotNetstat;

public static class OperatingSystem
{
    //from https://mariusschulz.com/blog/detecting-the-operating-system-in-net-core
    
    public static bool IsWindows() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static bool IsMacOSx() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool IsLinux() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}