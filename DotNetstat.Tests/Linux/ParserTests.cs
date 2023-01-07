using ConsoleTables;
using DotNetstat.Tests.Extensions;
using DotNetstat.Tests.Facts;
using DotNetstat.Tests.Resources;
using Xunit.Abstractions;

namespace DotNetstat.Tests.Linux;

public class ParserTests
{
    private readonly ITestOutputHelper _output;
    
    public ParserTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void TestParser()
    {
        var netstatOutput = TestResourceGetter.Get("linux.ss.ltnup.txt");
        var parser = ParserFactory.Get(Platform.Linux, false);
        var results = parser.Parse(netstatOutput);
        Assert.Equal(14, results.Lines.Count());
        Assert.True(results.Lines.First().ProcessId == 8675);
        Assert.All(results.Lines.ToArray()[2..^1], l=> Assert.Equal(0, l.ProcessId));
        Assert.True(results.Lines.Last().ProcessId == 309);

        _output.WriteLine(results.WriteLinesAndOriginalOutput());
    }
}