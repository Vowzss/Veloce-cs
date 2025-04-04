using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.interceptors.client;

namespace FaucetSharp.Gameplay.interceptors;

public sealed class FaucetClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public FaucetClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}