using FhnwAdSynchronizer.Verifiers;
using FhnwAdSynchronizer.Configuration;
using FhnwAdSynchronizer.Core;
using Kurukuru;
using Sharprompt;

namespace FhnwAdSynchronizer.Handlers.ConfigManagement;

[HandlerName("Remove folders for synchronization")]
public class RemoveFoldersForSynchronizationHandler : IHandler
{
    private readonly IConfigurationService<ConfigurationObject> _configuratonService;

    public RemoveFoldersForSynchronizationHandler(IConfigurationService<ConfigurationObject> configuratonService)
    {
        _configuratonService = configuratonService;
    }
    public async Task<bool> RunAsync()
    {
        var configurationObject = _configuratonService.ConfigurationObject;

        if (!configurationObject.FoldersToSynchronize.DoesFoldersToSynchronizeExists())
        {
            return true;
        }

        var foldersToRemove = Prompt.MultiSelect<FolderToSynchronize>("Select all folders for synchonization which should be removed from configuration",
                                                                      configurationObject.FoldersToSynchronize,
                                                                      null,
                                                                      1,
                                                                      int.MaxValue,
                                                                      null,
                                                                      folder => folder.Name);


        await Spinner.StartAsync("Removing folders for synchonization from configuration...", async spinner =>
        {
            var amountOfRemovedFolders = configurationObject.FoldersToSynchronize.RemoveAll(fts => foldersToRemove.Any(ftr => ftr.Id == fts.Id));

            if (amountOfRemovedFolders > 0)
            {
                await _configuratonService.UpdateConfigurationObject(configurationObject);
                var folderNamesToRemove = string.Join(", ", foldersToRemove.Select(ftr => ftr.Name));
                spinner.Succeed($"Folder(s) {folderNamesToRemove} successfully added!");
            }
            spinner.Info("No folders were removed from configuration...");
        });

        return true;
    }
}