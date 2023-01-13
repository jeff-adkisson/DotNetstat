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

    
    // [Fact]
    // public void TestParser()
    // {
    //     var netstatOutput = TestResourceGetter.Get("osx.netstat.nlp.txt");
    //     var parser = ParserFactory.Get(Platform.Osx, false);
    //     var results = parser.Parse(netstatOutput);
    //     var lines = results.Lines.ToArray();
    //     Assert.Equal(22, lines.Count());
    //     Assert.True(lines[2].ProcessId == 19201);
    //     Output.WriteLine(results.WriteLinesAndOriginalOutput());
    // }
}