using FhnwAdSynchronizer.Verifiers;
using FhnwAdSynchronizer.Configuration;
using FhnwAdSynchronizer.Core;
using FhnwAdSynchronizer.Extensions;
using Kurukuru;
using Sharprompt;

namespace FhnwAdSynchronizer.Handlers.FolderSynchronization;

[HandlerName("Synchronize folders")]
public class SynchronizeFolderHandler : IHandler
{
    private readonly IConfigurationService<ConfigurationObject> _configuratonService;

    public SynchronizeFolderHandler(IConfigurationService<ConfigurationObject> configuratonService)
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

        var foldersToSynchronize = Prompt.MultiSelect<FolderToSynchronize>("Select all folders which should be synchronized",
                                                                           configurationObject.FoldersToSynchronize,
                                                                           null,
                                                                           1,
                                                                           int.MaxValue,
                                                                           null,
                                                                           folder => folder.Name);

        foreach (var fts in foldersToSynchronize)
        {
            Spinner.Start($"Synchronize folder '{fts.Name}'", spinner =>
            {
                try
                {
                    var skipped = FileUtils.Copy(fts.SourceFolder, fts.TargetFolder);
                    spinner.Succeed($"Folder '{fts.Name}' successfully copied! {skipped.Count} file(s) skipped!");
                }
                catch (Exception ex)
                {
                    spinner.Fail(ex.Message);
                }
            });
        }

        return Task.FromResult(true);
    }
}