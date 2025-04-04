using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.configs;

public sealed class FaucetServerConfig : AbstractServerConfig
{
    public FaucetServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}