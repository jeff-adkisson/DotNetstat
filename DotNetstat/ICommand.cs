﻿namespace DotNetstat;

public interface ICommand
{
    Platform Platform { get; }
    string Shell { get; }
    string Name { get; }
    string Arguments { get; }
    CommandRegex Regex { get; }
}