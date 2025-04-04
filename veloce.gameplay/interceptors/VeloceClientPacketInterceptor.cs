using veloce.shared.handlers;
using veloce.shared.interceptors.client;

namespace veloce.gameplay.interceptors;

public sealed class VeloceClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public VeloceClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}