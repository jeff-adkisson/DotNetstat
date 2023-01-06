namespace DotNetstat;

internal static class NetstatFlavorExtensions
{
    public static Command Command(this Flavor flavor)
    {
        var cmdAttr = flavor.GetAttributeOfType<CommandAttribute>();
        return new Command(cmdAttr.Command, cmdAttr.Arguments);
    }

    public static Platform RelatedPlatform(this Flavor flavor)
    {
        var relatedAttr = flavor.GetAttributeOfType<RelatedPlatformAttribute>();
        return relatedAttr.Platform;
    }
}