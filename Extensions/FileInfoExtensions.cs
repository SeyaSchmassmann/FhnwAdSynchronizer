namespace FhnwAdSynchronizer.Extensions;

public static class FileInfoExtensions
{
    private static string[] mediaExtensions = {
        ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", ".M4A",
        ".AVI", ".MP4", ".DIVX", ".WMV",
    };
    private static string[] installationExtensions = {
        ".EXE", ".DEB", ".DMG", ".MSI", ".MSM", ".MSP", ".MST", ".IDT",
        ".CUB", ".PCP"
    };

    public static bool IsMediaFile(this FileInfo fileInfo)
    {
        return mediaExtensions.Contains(fileInfo.Extension.ToUpperInvariant());
    }

    public static bool IsInstallationFile(this FileInfo fileInfo)
    {
        return installationExtensions.Contains(fileInfo.Extension.ToUpperInvariant());
    }
}
