using FhnwAdSynchronizer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sharprompt;

namespace FhnwAdSynchronizer.Core;

public class HandlerSelector
{
    private static Func<Type, string> TextSelector = (Type type) => type.GetAttributeValue((HandlerNameAttribute handler) => handler.Description);

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HandlerSelector(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IHandler SelectHandler(string message, params Type[] handlers)
    {
        var handlerType = Prompt.Select<Type>(message, handlers, null, null, TextSelector);

        using (var serviceScope = _serviceScopeFactory.CreateScope())
        {
            return (IHandler)serviceScope.ServiceProvider.GetRequiredService(handlerType);
        }
    }
}