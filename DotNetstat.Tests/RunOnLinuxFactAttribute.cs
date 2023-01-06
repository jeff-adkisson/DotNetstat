namespace DotNetstat.Tests;

public sealed class RunOnLinuxFact : FactAttribute
{
    public RunOnLinuxFact()
    {
        if (PlatformAutoSelector.Select() != Platform.Linux) Skip = "Ignore on non-Linux platforms";
    }
}