using System.Reflection;
using FaucetSharp.Models.Packets;
using ProtoBuf.Meta;

namespace FaucetSharp.Core.Utils;

/// <summary>
///     A utility class for registering packet types with protobuf, ensuring compatibility for serialization and
///     deserialization.
/// </summary>
public static class PacketRegistry
{
    private static readonly Dictionary<Type, int> Indexes = new();

    static PacketRegistry()
    {
        // Load default packets within the library
        FindAndLoadPackets(Assembly.GetExecutingAssembly());
    }

    private static RuntimeTypeModel Registry { get; } = RuntimeTypeModel.Create();

    /// <summary>
    ///     A utility method to look for any packets within a given assembly and load them.
    /// </summary>
    public static void FindAndLoadPackets(Assembly assembly)
    {
        // Find packets within the assembly
        var packets = PacketResolver.Resolve(assembly);

        // Build all packets hierarchy
        var hierarchy = PacketResolver.BuildHierarchy(packets);

        // Register the packets within protobuf registry for dynamic resolution
        RegisterPackets(hierarchy);
    }

    /// <summary>
    ///     A utility method to register packet types for protobuf serialization and deserialization.
    /// </summary>
    private static void RegisterPacketType(Type baseType, Type packetType)
    {
        // Ensure base type implements IPacket
        if (!typeof(IPacket).IsAssignableFrom(baseType))
            throw new ArgumentException($"{baseType.Name} must implement IPacket");

        // Ensure packet type implements base type
        if (!baseType.IsAssignableFrom(packetType))
            throw new ArgumentException($"{packetType.Name} must inherit from {baseType.Name}");

        Indexes.TryAdd(baseType, 100);

        var index = Indexes[baseType];
        Registry.Add(baseType).AddSubType(index, packetType);
        Indexes[baseType] = index + 1;
    }

    /// <summary>
    ///     A utility method to recursively register a hierarchical packet node for protobuf serialization and deserialization.
    /// </summary>
    private static void RegisterPackets(HierarchyNode node)
    {
        node.Children.ForEach(children =>
        {
            // Register base types
            RegisterPacketType(node.Type, children.Type);

            // Register concrete types if any
            children.ConcreteList.ForEach(y => { RegisterPacketType(children.Type, y); });

            // Recursive call to make sure children are registered themselves as well
            RegisterPackets(children);
        });
    }

    /// <summary>
    ///     The only method to transform a packet into bytes using protobuf ensuring compatibility.
    /// </summary>
    public static byte[] Serialize(IPacket packet)
    {
        try
        {
            using var stream = new MemoryStream();
            Registry.Serialize(stream, packet);
            return stream.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     The only method to transform bytes into a packet using protobuf ensuring compatibility.
    /// </summary>
    public static AbstractPacket Deserialize(byte[] data)
    {
        try
        {
            return Registry.Deserialize<AbstractPacket>(new ReadOnlyMemory<byte>(data));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}