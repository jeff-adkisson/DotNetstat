namespace DotNetstat.Tests.Facts;

public sealed class RunOnWindowsFactAttribute : FactAttribute
{
    public RunOnWindowsFactAttribute()
    {
        if (PlatformDetector.Detect() != Platform.Windows) Skip = "Ignore on non-Windows platforms";
    }
}