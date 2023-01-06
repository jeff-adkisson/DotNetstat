namespace DotNetstat.Tests;

public sealed class RunOnOsx : FactAttribute
{
    public RunOnOsx()
    {
        if (PlatformAutoSelector.Select() != Platform.Osx)
        {
            Skip = "Ignore on non-OSx platforms";
        }
    }
}