using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.interceptors.client;

namespace FaucetSharp.Gameplay.interceptors;

public sealed class VeloceClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public VeloceClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}