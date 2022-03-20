namespace FhnwAdSynchronizer.Core;

public class HandlerNameAttribute : Attribute
{
    public string Description { get; }

    public HandlerNameAttribute(string description)
    {
        Description = description;
    }
}