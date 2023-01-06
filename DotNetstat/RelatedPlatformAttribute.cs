namespace DotNetstat;

public sealed class RelatedPlatformAttribute : Attribute
{
    public RelatedPlatformAttribute(Platform platform)
    {
        Platform = platform;
    }

    public Platform Platform { get; }
}