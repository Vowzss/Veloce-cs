using veloce.shared.packets;

namespace veloce.shared.events.server;

public delegate void ConnectEvent(IConnectPacket packet);
public delegate void DisconnectEvent(IDisconnectPacket packet);
public delegate void ReconnectEvent(IReconnectPacket packet);

public delegate void PongEvent(IPongPacket packet);