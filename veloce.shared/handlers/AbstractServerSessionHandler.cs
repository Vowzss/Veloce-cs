using veloce.shared.models;

namespace veloce.shared.handlers;

public abstract class AbstractServerSessionHandler : AbstractSessionHandler<IServerSession>, IServerSessionHandler
{
    
}

public sealed class DefaultServerSessionHandler : AbstractServerSessionHandler
{
    
}