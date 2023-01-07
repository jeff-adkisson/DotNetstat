namespace DotNetstat.Tests.Automatic;

public class AutomaticTests
{
    [Fact]
    public void TestAutomatic()
    {
        var results = Netstat.Call();
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least on result");
    }

    [Fact]
    public void TestPlatformAutomatic()
    {
        var results = Netstat.Call(Platform.Automatic);
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least on result");
    }

    [Fact]
    public void TestFlavorAutomatic()
    {
        var results = Netstat.Call();
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least on result");
    }
}