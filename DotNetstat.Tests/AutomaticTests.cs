namespace DotNetstat.Tests;

public class AutomaticTests
{
    [Fact]
    public void TestAutomatic()
    {
        var results = NetStat.Call();
        Assert.True(results.Any(), "Expected at least on result");
    }

    [Fact] public void TestPlatformAutomatic()
    {
        var results = NetStat.Call(Platform.Automatic);
        Assert.True(results.Any(), "Expected at least on result");
    }
    
    [Fact] public void TestFlavorAutomatic()
    {
        var results = NetStat.Call(Flavor.Automatic);
        Assert.True(results.Any(), "Expected at least on result");
    }
}