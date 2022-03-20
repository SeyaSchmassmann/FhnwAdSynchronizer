namespace FhnwAdSynchronizer.Extensions;

public static class FileInfoExtensions
{
    private static string[] mediaExtensions = {
        ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", ".M4A",
        ".AVI", ".MP4", ".DIVX", ".WMV",
    };

    public static bool IsMediaFile(this FileInfo fileInfo)
    {
        return mediaExtensions.Contains(fileInfo.Extension.ToUpperInvariant());
    }
}