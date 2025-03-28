using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void ConnectEvent(IConnectPacket packet);
public delegate void DisconnectEvent(IDisconnectPacket packet);
public delegate void ReconnectEvent(IReconnectPacket packet);

public delegate void TickEvent();
public delegate void TickMissedEvent(long time);

public delegate void PongEvent(IPongPacket packet);