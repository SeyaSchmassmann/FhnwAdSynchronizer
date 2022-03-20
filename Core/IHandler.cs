namespace FhnwAdSynchronizer.Core;

public interface IHandler
{
    Task<bool> RunAsync();
}