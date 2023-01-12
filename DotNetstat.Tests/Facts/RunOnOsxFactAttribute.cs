namespace DotNetstat.Tests.Facts;

public sealed class RunOnOsxFactAttribute : FactAttribute
{
    public RunOnOsxFactAttribute()
    {
        if (PlatformDetector.Detect() != Platform.Osx) Skip = "Ignore on non-OSx platforms";
    }
}