using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.configs;

public sealed class FaucetClientConfig : AbstractClientConfig
{
    public FaucetClientConfig(Guid playerId) : base(playerId)
    {

    }
}