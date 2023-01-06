namespace DotNetstat.Tests;

public sealed class RunOnLinux : FactAttribute
{
    public RunOnLinux()
    {
        if (PlatformAutoSelector.Select() != Platform.Linux)
        {
            Skip = "Ignore on non-Linux platforms";
        }
    }
}