using FhnwAdSynchronizer.Core;

namespace FhnwAdSynchronizer.Extensions;

public static class AttributeExtensions
{
    public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector)
        where TAttribute : Attribute
    {
        var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

        if (att != null)
        {
            return valueSelector(att);
        }

        throw new Exception($"Handler is not decorated with {typeof(HandlerNameAttribute)}");
    }
}