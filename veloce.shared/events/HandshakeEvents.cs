using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void FirstHandshakeEvent(IFirstHandshakePacket packet);
public delegate void SecondHandshakeEvent(ISecondHandshakePacket packet);