namespace veloce.shared.attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class PacketIdentifierAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}