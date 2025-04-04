using FaucetSharp.Models.Objects.Config.Client;

namespace FaucetSharp.Gameplay.Configs;

public sealed class FaucetClientConfig : AbstractClientConfig
{
    public FaucetClientConfig(Guid playerId) : base(playerId)
    {

    }
}