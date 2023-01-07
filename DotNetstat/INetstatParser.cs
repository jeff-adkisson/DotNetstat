namespace DotNetstat;

public interface INetstatParser
{
    INetstatOutput Parse(string netstatCmdOutput);
}