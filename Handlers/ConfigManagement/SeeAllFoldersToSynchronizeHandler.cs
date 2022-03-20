using FhnwAdSynchronizer.Verifiers;
using FhnwAdSynchronizer.Configuration;
using FhnwAdSynchronizer.Core;
using Sharprompt;

namespace FhnwAdSynchronizer.Handlers.ConfigManagement;

[HandlerName("Show folders for synchronization")]
public class ShowFoldersForSynchronizationHandler : IHandler
{
    private readonly IConfigurationService<ConfigurationObject> _configuratonService;

    public ShowFoldersForSynchronizationHandler(IConfigurationService<ConfigurationObject> configuratonService)
    {
        _configuratonService = configuratonService;
    }

    public Task<bool> RunAsync()
    {
        var configurationObject = _configuratonService.ConfigurationObject;

        if (!configurationObject.FoldersToSynchronize.DoesFoldersToSynchronizeExists())
        {
            return Task.FromResult(true);
        }

        foreach (var fts in configurationObject.FoldersToSynchronize)
        {
            Console.WriteLine($"* {fts.Name}");
        }
        return Task.FromResult(true);
    }
}