namespace DotNetstat.Tests;


public class WindowsTests
{
    [RunOnWindowsFact]
    public void TestPlatform()
    {
        var results = NetStat.Call(Platform.Windows);
        Assert.True(results.Any(), "Expected at least on result");
    }

    [RunOnWindowsFact]
    public void TestFlavor()
    {
        var results = NetStat.Call(Flavor.WindowsNetstatNao);
        Assert.True(results.Any(), "Expected at least on result");
    }
}