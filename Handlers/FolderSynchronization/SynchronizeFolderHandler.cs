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

        Spinner.Start("Synchronize folders...", spinner =>
        {
            foreach (var fts in foldersToSynchronize)
            {
                try
                {
                    var skipped = FileUtils.Copy(fts.SourceFolder, fts.TargetFolder, spinner);
                    var skippedText = skipped.Count > 0 ? $"{skipped.Count} file(s) were skipped" : "";
                    spinner.Succeed($"Folder(s) successfully copied! {skippedText}");
                }
                catch (Exception ex)
                {
                    spinner.Fail(ex.Message);
                }
            }
        });

        return Task.FromResult(true);
    }
}