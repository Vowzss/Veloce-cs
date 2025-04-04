using veloce.shared.models;

namespace veloce.gameplay.configs;

public sealed class VeloceServerConfig : AbstractServerConfig
{
    public VeloceServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}