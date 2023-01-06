namespace DotNetstat;

public sealed class RelatedPlatformAttribute : Attribute
{
    public Platform Platform { get; }

    public RelatedPlatformAttribute(Platform platform)
    {
        Platform = platform;
    }
}