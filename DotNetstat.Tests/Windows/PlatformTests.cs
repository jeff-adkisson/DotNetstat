using DotNetstat.Tests.Extensions;
using DotNetstat.Tests.Facts;
using Xunit.Abstractions;

namespace DotNetstat.Tests.Windows;

public class PlatformTests : TestBase
{
    public PlatformTests(ITestOutputHelper output) : base(output)
    {
    }

    [RunOnWindowsFact]
    public void TestPlatform()
    {
        var isWinCmd = Platform.Automatic.GetCommand();
        Assert.True(isWinCmd.Platform == Platform.Windows);
        
        var results = Netstat.Call(Platform.Windows);
        Assert.True(results.Success);
        Assert.True(results.Lines.Any(), "Expected at least one result");
        Assert.Contains(results.Lines, p => p.Process != null);

        Output.WriteLine(results.WriteLinesAndOriginalOutput());

        var resultsWithoutProcesses =
            Netstat.Call(Platform.Windows, false);
        Assert.True(resultsWithoutProcesses.Success);
        Assert.All(resultsWithoutProcesses.Lines, p => Assert.Null(p.Process));
    }
}