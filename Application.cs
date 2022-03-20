using FhnwAdSynchronizer.Core;
using FhnwAdSynchronizer.Handlers;
using FhnwAdSynchronizer.Handlers.ConfigManagement;
using FhnwAdSynchronizer.Handlers.FolderSynchronization;

namespace FhnwAdSynchronizer;

public class Application
{
    private readonly HandlerSelector _handlerSelector;

    public Application(HandlerSelector handlerSelector)
    {
        _handlerSelector = handlerSelector;
    }

    public async Task RunAsync(string[] args)
    {
        var isRunning = true;
        while (isRunning)
        {
            var handler = _handlerSelector.SelectHandler("Select operation", typeof(ManageFoldersForSynchronizationHandler),
                                                                             typeof(SynchronizeFolderHandler),
                                                                             typeof(ExitApplicationHandler));
            isRunning = await handler.RunAsync();
        }
    }
}
