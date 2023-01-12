namespace DotNetstat.Tests.Facts;

public sealed class RunOnLinuxFact : FactAttribute
{
    public RunOnLinuxFact()
    {
        if (PlatformDetector.Detect() != Platform.Linux) Skip = "Ignore on non-Linux platforms";
    }
}