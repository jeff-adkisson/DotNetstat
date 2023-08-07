using DotNetstat.NetstatParsing;
using DotNetstat.Tests.Extensions;
using DotNetstat.Tests.Resources;
using Xunit.Abstractions;

namespace DotNetstat.Tests.OSx;

public class ParserTests : TestBase
{
    public ParserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestParser()
    {
        var netstatOutput = TestResourceGetter.Get("osx.lsof.i4a.txt");
        var parser = ParserFactory.Get(Platform.Osx, false);
        var results = parser.Parse(netstatOutput);
        var lines = results.Lines.ToArray();
        Output.WriteLine(results.WriteLinesAndOriginalOutput());
        Assert.Equal(72, lines.Count());
        Assert.Equal(1178, lines[0].ProcessId);
        Assert.Equal(875, lines[1].ProcessId);
    }
}