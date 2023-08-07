using DotNetstat.Tests.Facts;

namespace DotNetstat.Tests.OSx;

public class PlatformTests
{
    [RunOnOsxFact]
    public void TestPlatform()
    {
        var isOsxCmd = Platform.Automatic.GetCommand();
        Assert.True(isOsxCmd.Platform == Platform.Osx);

        var results = Netstat.Call(Platform.Osx);
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least one result");
        Assert.Contains(results.Lines, p => p.Process != null);

        var resultsWithoutProcesses =
            Netstat.Call(Platform.Osx, false);
        Assert.True(resultsWithoutProcesses.Success);
        Assert.All(resultsWithoutProcesses.Lines, p => Assert.Null(p.Process));
    }
}