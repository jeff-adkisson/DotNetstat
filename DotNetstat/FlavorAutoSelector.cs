namespace DotNetstat;

public static class FlavorAutoSelector
{
    public static string Select(this Flavor flavor)
    {
        if (flavor != Flavor.Automatic) return flavor.GetCommand();
        
        var platform = PlatformAutoSelector.Select();
        switch (platform)
        {
            case Platform.Windows:
                return Flavor.WindowsNetstatNao.GetCommand();
            case Platform.Linux:
                return Flavor.LinuxNetstatNao.GetCommand();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}