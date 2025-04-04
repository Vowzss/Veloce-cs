using FaucetSharp.Models.Objects.Config.Server;

namespace FaucetSharp.Gameplay.Configs;

public sealed class FaucetServerConfig : AbstractServerConfig
{
    public FaucetServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}