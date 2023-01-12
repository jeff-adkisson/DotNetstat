using DotNetstat.NetstatParsing;
using DotNetstat.Tests.Extensions;
using DotNetstat.Tests.Resources;
using Xunit.Abstractions;

namespace DotNetstat.Tests.Windows;

public class ParserTests : TestBase
{
    public ParserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestParser()
    {
        var netstatOutput = TestResourceGetter.Get("windows.netstat.n.a.o.txt");
        var parser = ParserFactory.Get(Platform.Windows, false);
        var results = parser.Parse(netstatOutput);
        Assert.Equal(326, results.Lines.Count());

        Output.WriteLine(results.WriteLinesAndOriginalOutput());
    }
}