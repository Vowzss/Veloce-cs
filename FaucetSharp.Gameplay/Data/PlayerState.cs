using System.Numerics;

namespace FaucetSharp.Gameplay.Data;

public sealed class PlayerState
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Velocity { get; set; } = Vector3.Zero;

    public float Health { get; set; } = 100;
}