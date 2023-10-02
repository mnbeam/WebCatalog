using System.ComponentModel;

namespace WebCatalog.Logic.Common.Extensions;

public static class EnumDescriptionExtension
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
            throw new ArgumentException("Can not get custom attribute.");
        
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is
            DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        throw new ArgumentException($"{enumValue} has not description");
    }
}