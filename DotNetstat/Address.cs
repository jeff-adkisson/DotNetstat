using System.Text.RegularExpressions;

namespace DotNetstat;

public record Address
{
    public Address(string address, Regex? extractPortRegex = null)
    {
        address = (address ?? "").Trim();

        if (string.IsNullOrWhiteSpace(address) || extractPortRegex == null)
        {
            Name = address;
            return;
        }

        var port = PortNotSpecified;
        var matches = extractPortRegex.Matches(address);
        if (matches.Count == 1) {
            var groups = matches[0].Groups;
            if (groups["address"].Success) address = groups["address"].Value;
            if (groups["port"].Success) int.TryParse(groups["port"].Value, out port);
        }
        
        Port = port;
        Name = address;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Returned if address does not have a port or the port cannot be parsed
    /// </summary>
    public static int PortNotSpecified => -1;

    public int Port { get; } = PortNotSpecified;

    public string Name { get; init; } = "";
}