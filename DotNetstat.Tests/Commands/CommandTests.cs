﻿using Xunit.Abstractions;

namespace DotNetstat.Tests.Commands;

public class CommandTests : TestBase
{
    public CommandTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void OutputCommands()
    {
        foreach (var command in DotNetstat.Commands.All) {
            Output.WriteLine($"{command.Platform} | {command.Regex}");
        }
    }
}