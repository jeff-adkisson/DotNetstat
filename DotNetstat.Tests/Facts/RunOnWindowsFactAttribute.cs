namespace DotNetstat.Tests.Facts;

public sealed class RunOnWindowsFactAttribute : FactAttribute
{
    public RunOnWindowsFactAttribute()
    {
        if (PlatformAutoSelector.Select() != Platform.Windows) Skip = "Ignore on non-Windows platforms";
    }
}