using DotNetstat.Tests.Facts;
using DotNetstat.Tests.Resources;

namespace DotNetstat.Tests.Windows;

public class ParserTests
{
    [Fact]
    public void TestParser()
    {
        var netstatOutput = TestResourceGetter.Get("windows.netstat.n.a.o.txt");
        var parser = ParserFactory.Get(Platform.Windows, false);
        var results = parser.Parse(netstatOutput);
        Assert.Equal(283, results.Lines.Count());
    }
}