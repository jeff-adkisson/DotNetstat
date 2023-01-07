using DotNetstat.Tests.Facts;

namespace DotNetstat.Tests.Windows;

public class PlatformTests
{
    [RunOnWindowsFact]
    public void TestPlatform()
    {
        var results = Netstat.Call(Platform.Windows);
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least one result");
        Assert.Contains(results.Lines, p => p.Process != null);

        var resultsWithoutProcesses =
            Netstat.Call(Platform.Windows, false);
        Assert.True(resultsWithoutProcesses.Success);
        Assert.All(resultsWithoutProcesses.Lines, p => Assert.Null(p.Process));
    }
}