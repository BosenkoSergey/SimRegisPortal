using System.ComponentModel;
using System.Reflection;

namespace SimRegisPortal.Core.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return value.ToString();

        var field = type.GetField(name);
        var attr = field?.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? name;
    }
}