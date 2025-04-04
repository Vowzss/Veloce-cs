using FaucetSharp.Core.Handlers;
using FaucetSharp.Core.interceptors.server;

namespace FaucetSharp.Gameplay.Interceptors;

public sealed class FaucetServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public FaucetServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}