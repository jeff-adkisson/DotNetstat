namespace DotNetstat;

public static class FlavorAutoSelector
{
    public static Command Command(this Platform platform)
    {
        if (platform == Platform.Automatic)
            return Flavor.Automatic.Command();

        return platform switch
        {
            Platform.Windows => Flavor.WindowsNetstatNao.Command(),
            Platform.Linux => Flavor.LinuxNetstatNao.Command(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}