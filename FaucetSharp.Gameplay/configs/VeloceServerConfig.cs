using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.configs;

public sealed class VeloceServerConfig : AbstractServerConfig
{
    public VeloceServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}