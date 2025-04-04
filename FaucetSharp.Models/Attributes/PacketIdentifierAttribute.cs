namespace FaucetSharp.Models.Attributes;

/// <summary>
///     Custom attribute to decorate any packet.
/// </summary>
/// <remarks>Any packets must be decorated with this attribute otherwise they won't be recognized.</remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class PacketIdentifierAttribute(string id) : Attribute
{
    public string Id { get; } = id;
}