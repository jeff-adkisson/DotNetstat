namespace DotNetstat;

public interface INetstatParser
{
    IEnumerable<NetstatLine> Parse(string netstatCmdOutput);
}