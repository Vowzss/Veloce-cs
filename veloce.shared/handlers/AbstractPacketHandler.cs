using ProtoBuf.Meta;
using veloce.shared.utils;

namespace veloce.shared.handlers;

public abstract class AbstractPacketHandler : IPacketHandler
{
    protected static RuntimeTypeModel Registry { get; } = RuntimeTypeModel.Create();
    private static readonly Dictionary<Type, int> Indexes = new();
    
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
}