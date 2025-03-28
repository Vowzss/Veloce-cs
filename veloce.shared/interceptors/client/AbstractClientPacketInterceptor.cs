﻿using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors.client;

public abstract class AbstractClientPacketInterceptor : IClientPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }
    
    public FirstHandshakeEvent? OnFirstHandshake { get; set;  }
    public SecondHandshakeEvent? OnSecondHandshake { get; set; }

    public PingEvent? OnPing { get; set; }
    
    protected AbstractClientPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }

    public void Accept(byte[] data, EncryptionContext? encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(data, encryption);
        
        // Match against default packets
        switch (packet)
        {
            case IPingPacket p:
                OnPing?.Invoke(p);
                return;
            
            default:
                Handle(packet);
                return;
        }
    }

    public virtual void Handle(IPacket packet) { }
}

public sealed class DefaultClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public DefaultClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}