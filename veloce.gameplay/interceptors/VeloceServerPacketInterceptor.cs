using veloce.shared.handlers;
using veloce.shared.interceptors.server;

namespace veloce.gameplay.interceptors;

public sealed class VeloceServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public VeloceServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}