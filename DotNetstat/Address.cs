namespace DotNetstat;

public record Address
{
    public Address(string address)
    {
        address = (address ?? "").Trim();
        if (string.IsNullOrWhiteSpace(address)) return;

        var parts = address.Split(':');
        if (parts.Length != 2)
        {
            Name = address;
            return;
        }

        var parsed = int.TryParse(parts[1], out var port);
        Port = parsed ? port : PortNotSpecified;
        Name = parts[0];
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Returned if address does not have a port or the port cannot be parsed
    /// </summary>
    public static int PortNotSpecified => -1;

    public int Port { get; init; } = PortNotSpecified;

    public string Name { get; init; } = "";
}