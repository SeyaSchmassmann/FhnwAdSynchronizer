using Kurukuru;

namespace FhnwAdSynchronizer.Extensions;

public class FileUtils
{
    public static List<string> Copy(string sourceDirectory, string targetDirectory)
    {
        var sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
        var targetDirectoryInfo = new DirectoryInfo(targetDirectory);

        return CopyInternal(sourceDirectoryInfo, targetDirectoryInfo, new());
    }

    private static List<string> CopyInternal(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, List<string> skipped)
    {
        if (targetDirectoryInfo.Exists)
        {
            targetDirectoryInfo.Attributes &= ~FileAttributes.ReadOnly;
        }
        else
        {
            targetDirectoryInfo.Create();
        }

        foreach (var sourceFileInfo in sourceDirectoryInfo.GetFiles())
        {
            var targetFilePath = Path.Combine(targetDirectoryInfo.FullName, sourceFileInfo.Name);
            var targetFileInfo = File.Exists(targetFilePath) ? new FileInfo(targetFilePath) : null;

            if (ShouldFileBeSkipped(sourceFileInfo))
            {
                skipped.Add(sourceFileInfo.Name);
                continue;
            }

            if (targetFileInfo != null)
            {
                targetFileInfo.Attributes &= ~FileAttributes.ReadOnly;
            }

            sourceFileInfo.CopyTo(targetFilePath, true);
        }

        foreach (var diSourceSubDir in sourceDirectoryInfo.GetDirectories())
        {
            var nextTargetSubDir = targetDirectoryInfo.CreateSubdirectory(diSourceSubDir.Name);
            CopyInternal(diSourceSubDir, nextTargetSubDir, skipped);
        }
        return skipped;
    }

    private static bool ShouldFileBeSkipped(FileInfo sourceFile)
    {
        if (sourceFile.IsMediaFile() || sourceFile.IsInstallationFile() || sourceFile.Extension == ".DS_Store")
        {
            return true;
        }
        return false;
    }
}