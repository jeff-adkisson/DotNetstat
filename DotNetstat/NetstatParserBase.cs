namespace DotNetstat;

internal abstract class NetstatParserBase : INetstatParser
{
    protected NetstatParserBase(bool includeProcessDetails)
    {
        IncludeProcessDetails = includeProcessDetails;
    }

    protected bool IncludeProcessDetails { get; init; }

    public abstract IEnumerable<NetstatLine> Parse(string netstatCmdOutput);
}