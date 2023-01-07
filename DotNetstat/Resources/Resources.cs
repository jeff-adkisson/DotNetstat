using System.Reflection;

namespace DotNetstat.Tests.Helpers;

public static class ResourceGetter
{
    private const string ResourcePath = "DotNetstat.Resources";

    /// <summary>
    ///     Gets an embedded resource as a string.
    /// </summary>
    /// <param name="resourceName">Name including extension without path</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string Get(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        resourceName = $"{ResourcePath}.{resourceName}";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(
            stream ??
            throw new InvalidOperationException($"Resource [{resourceName}] not found"));
        return reader.ReadToEnd();
    }

    /// <summary>
    ///     Returns a list of all embedded resources
    /// </summary>
    /// <returns></returns>
    public static List<string> List()
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetManifestResourceNames().ToList();
    }
}