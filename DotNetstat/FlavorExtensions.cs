using System.ComponentModel;

namespace DotNetstat;

public static class NetstatFlavorExtensions
{
    public static string GetCommand(this NetstatFlavor val)
    {
        var attributes = (DescriptionAttribute[])val
            .GetType()
            .GetField(val.ToString())!
            .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}