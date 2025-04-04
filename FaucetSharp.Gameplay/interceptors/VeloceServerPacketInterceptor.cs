using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.interceptors.server;

namespace FaucetSharp.Gameplay.interceptors;

public sealed class FaucetServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public FaucetServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}