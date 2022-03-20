using Kurukuru;

namespace FhnwAdSynchronizer.Extensions;

public class FileUtils
{
    public static List<string> Copy(string sourceDirectory, string targetDirectory, Spinner spinner)
    {
        var sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
        var targetDirectoryInfo = new DirectoryInfo(targetDirectory);

        return CopyInternal(sourceDirectoryInfo, targetDirectoryInfo, spinner, new());
    }

    private static List<string> CopyInternal(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, Spinner spinner, List<string> skipped)
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

            if (targetFileInfo != null)
            {
                if (ShouldFileBeSkipped(sourceFileInfo, targetFileInfo))
                {
                    skipped.Add(sourceFileInfo.Name);
                    continue;
                }

                targetFileInfo.Attributes &= ~FileAttributes.ReadOnly;
            }

            spinner.Text = $"Copying file {sourceFileInfo.Name} from {sourceDirectoryInfo.FullName} to {targetDirectoryInfo.FullName}";
            sourceFileInfo.CopyTo(targetFilePath, true);
        }

        foreach (var diSourceSubDir in sourceDirectoryInfo.GetDirectories())
        {
            var nextTargetSubDir = targetDirectoryInfo.CreateSubdirectory(diSourceSubDir.Name);
            CopyInternal(diSourceSubDir, nextTargetSubDir, spinner, skipped);
        }
        return skipped;
    }

    private static bool ShouldFileBeSkipped(FileInfo sourceFile, FileInfo targetFile)
    {
        if (sourceFile.IsMediaFile())
        {
            return true;
        }
        return false;
    }
}