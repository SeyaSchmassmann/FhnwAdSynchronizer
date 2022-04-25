using FhnwAdSynchronizer.Configuration;
using FhnwAdSynchronizer.Core;
using Kurukuru;
using Sharprompt;

namespace FhnwAdSynchronizer.Handlers.ConfigManagement;

[HandlerName("Add new folder for synchronization")]
public class AddFolderForSynchronizationHandler : IHandler
{
    private readonly IConfigurationService<ConfigurationObject> _configuratonService;

    public AddFolderForSynchronizationHandler(IConfigurationService<ConfigurationObject> configuratonService)
    {
        _configuratonService = configuratonService;
    }

    public async Task<bool> RunAsync()
    {
        var name = Prompt.Input<string>("Purpose/Subject of the folder");
        var sourceFolder = Prompt.Input<string>("Source folder path");
        var targetFolder = Prompt.Input<string>("Target folder path");

        var configurationObject = _configuratonService.ConfigurationObject;
        await Spinner.StartAsync("Adding folder for synchonization to configuration...", async spinner =>
        {
            configurationObject.FoldersToSynchronize.Add(new(Guid.NewGuid(), name, sourceFolder, targetFolder));
            await _configuratonService.UpdateConfigurationObject(configurationObject);
            spinner.Succeed($"Folder {name} successfully added!");
        });

        return true;
    }
}