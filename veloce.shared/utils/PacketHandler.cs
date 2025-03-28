using Microsoft.Win32;
using ProtoBuf.Meta;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents a unique object for packet serialization and deserialization.
/// </summary>
/// <remarks>This object is a wrapper around protobuf to dynamically manage packet resolution.</remarks>
public static class PacketHandler
{
    private static RuntimeTypeModel Registry { get; } = RuntimeTypeModel.Create();
    private static readonly IDictionary<Type, int> Indexes = new Dictionary<Type, int>();
    
    /// <summary>
    /// Method to register packet types.
    /// </summary>
    public static void RegisterPacketType<TPacketBase, TPacket>()
        where TPacketBase : class
        where TPacket : TPacketBase
    {
        if (!Indexes.ContainsKey(typeof(TPacketBase)))
            Indexes.Add(typeof(TPacketBase), 100);
        
        var index = Indexes[typeof(TPacketBase)];
        Registry.Add<TPacketBase>()
            .AddSubType(index, typeof(TPacket));
        Indexes[typeof(TPacketBase)] = index + 1;
    }
    
    /// <summary>
    /// Method to transform a packet into a binary format.
    /// </summary>
    public static byte[] Write<TPacket>(TPacket packet) where TPacket : class, IPacket
    {
        using var stream = new MemoryStream();
        Registry.Serialize(stream, packet);
        return stream.ToArray();
    }

    /// <summary>
    /// Method to transform a binary format into a packet.
    /// </summary>
    public static IPacket Read(byte[] data)
    {
        using var stream = new MemoryStream(data);
        return Registry.Deserialize<AbstractPacket>(stream);
    }
}