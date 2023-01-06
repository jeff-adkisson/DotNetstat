namespace DotNetstat.Tests;

public sealed class RunOnWindowsFact : FactAttribute
{
    public RunOnWindowsFact()
    {
        if (PlatformAutoSelector.Select() != Platform.Windows)
        {
            Skip = "Ignore on non-Windows platforms";
        }
    }
}