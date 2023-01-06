namespace DotNetstat.Tests;

public sealed class RunOnOsxFactAttribute : FactAttribute
{
    public RunOnOsxFactAttribute()
    {
        if (PlatformAutoSelector.Select() != Platform.Osx) Skip = "Ignore on non-OSx platforms";
    }
}