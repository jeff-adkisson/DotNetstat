namespace DotNetstat.Tests;

public class AutomaticTests
{
    [RunOnLinux]
    public void TestPlatform()
    {
        var results = NetStat.Call(Platform.Linux);
        Assert.True(results.Any(), "Expected at least on result");
    }

    [RunOnLinux]
    public void TestFlavor()
    {
        var results = NetStat.Call(Flavor.LinuxNetstatNao);
        Assert.True(results.Any(), "Expected at least on result");
    }
}