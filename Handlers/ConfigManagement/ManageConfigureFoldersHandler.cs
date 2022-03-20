using FhnwAdSynchronizer.Core;

namespace FhnwAdSynchronizer.Handlers.ConfigManagement;

[HandlerName("Manage folders for synchronization")]
public class ManageFoldersForSynchronizationHandler : IHandler
{
    private readonly HandlerSelector _handlerSelector;

    public ManageFoldersForSynchronizationHandler(HandlerSelector handlerSelector)
    {
        _handlerSelector = handlerSelector;
    }

    public async Task<bool> RunAsync()
    {
        var handler = _handlerSelector.SelectHandler("Select operation", typeof(ShowFoldersForSynchronizationHandler),
                                                                         typeof(AddFolderForSynchronizationHandler),
                                                                         typeof(RemoveFoldersForSynchronizationHandler));
        return await handler.RunAsync();
    }
}