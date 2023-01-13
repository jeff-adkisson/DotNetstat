using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetstat.Models;

namespace DotNetstat.Configuration;

internal static class Loader
{
    private const string ResourcePath = "DotNetstat.Configuration";

    private const string CommandsFile = "configuration.json";

    internal static ReadOnlyCollection<ICommand> GetConfiguration()
    {
        var commandJson = Get(CommandsFile);

        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        var config = JsonSerializer.Deserialize<ConfigurationModel>(commandJson, options);
        return config!
            .Commands
            .Select(c => new Command(c) as ICommand)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    ///     Gets an embedded resource as a string.
    /// </summary>
    /// <param name="resourceName">Name including extension without path</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static string Get(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        resourceName = $"{ResourcePath}.{resourceName}";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(
            stream ??
            throw new InvalidOperationException($"Resource [{resourceName}] not found"));
        return reader.ReadToEnd();
    }
}