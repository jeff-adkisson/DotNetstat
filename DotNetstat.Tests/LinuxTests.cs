namespace DotNetstat.Tests;

public class LinuxTests
{
    [RunOnLinuxFact]
    public void TestPlatform()
    {
        var results = Netstat.Call(Platform.Linux);
        Assert.True(results.Any(), "Expected at least on result");
    }

    [RunOnLinuxFact]
    public void TestFlavor()
    {
        var results = Netstat.Call(Flavor.LinuxNetstatNao);
        Assert.True(results.Any(), "Expected at least on result");
    }
}