namespace FaucetSharp.Models.Events;

public delegate void TickEvent();

public delegate void TickMissedEvent(long time);