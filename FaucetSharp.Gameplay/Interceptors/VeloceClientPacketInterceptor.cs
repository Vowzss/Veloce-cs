using FaucetSharp.Core.Handlers;
using FaucetSharp.Core.interceptors.client;

namespace FaucetSharp.Gameplay.Interceptors;

public sealed class FaucetClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public FaucetClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}