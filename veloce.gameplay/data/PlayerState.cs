using System.Numerics;

namespace veloce.gameplay.data;

public sealed class PlayerState
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Velocity { get; set; } = Vector3.Zero;

    public float Health { get; set; } = 100;
}