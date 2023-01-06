namespace DotNetstat.Tests;

public class WindowsTests
{
    [RunOnWindowsFact]
    public void TestPlatform()
    {
        var results = Netstat.Call(Platform.Windows).ToList();
        Assert.True(results.Any(), "Expected at least one result");
        Assert.Contains(results, p => p.Process != null);

        var resultsWithoutProcesses =
            Netstat.Call(Platform.Windows, false).ToList();
        Assert.All(resultsWithoutProcesses, p => Assert.Null(p.Process));
    }

    [RunOnWindowsFact]
    public void TestFlavor()
    {
        var results = Netstat.Call(Flavor.WindowsNetstatNao).ToList();
        Assert.True(results.Any(), "Expected at least on result");
        Assert.Contains(results, p => p.Process != null);

        var resultsWithoutProcesses =
            Netstat.Call(Flavor.WindowsNetstatNao, false).ToList();
        Assert.All(resultsWithoutProcesses, p => Assert.Null(p.Process));
    }
}