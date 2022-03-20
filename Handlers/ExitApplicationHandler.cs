using FhnwAdSynchronizer.Core;

namespace FhnwAdSynchronizer.Handlers;

[HandlerName("Exit application")]
public class ExitApplicationHandler : IHandler
{
    public Task<bool> RunAsync()
    {
        return Task.FromResult(false);
    }
}