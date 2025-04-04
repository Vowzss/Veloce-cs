using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.interceptors.server;

namespace FaucetSharp.Gameplay.interceptors;

public sealed class VeloceServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public VeloceServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}