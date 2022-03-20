using FhnwAdSynchronizer.Configuration;

namespace FhnwAdSynchronizer.Verifiers;

public static class FoldersToSynchronizeVerifier
{
    public static bool DoesFoldersToSynchronizeExists(this List<FolderToSynchronize> foldersToSynchronize)
    {
        if (foldersToSynchronize.Count == 0)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[i] ");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine("No folders available for synchronization!");
            return false;
        }
        return true;
    }
}
