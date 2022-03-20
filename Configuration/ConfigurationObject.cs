namespace FhnwAdSynchronizer.Configuration;

public class ConfigurationObject
{
    public List<FolderToSynchronize> FoldersToSynchronize { get; set; } = new();
}

public record FolderToSynchronize(Guid Id, string Name, string TargetFolder, string SourceFolder);