using System;
using System.ComponentModel;
using System.Reflection;

namespace Indigo.Infrastructure.Util
{
    public static class ReflectionExtensions
    {
        public static string GetDisplayName(this ICustomAttributeProvider attributeProvider)
        {
            var attrs = (DisplayNameAttribute[])attributeProvider.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            return attrs.Length > 0 ? attrs[0].DisplayName : null;
        }

        public static string GetDescription(this ICustomAttributeProvider attributeProvider)
        {
            var attrs = (DescriptionAttribute[])attributeProvider.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attrs.Length > 0 ? attrs[0].Description : null;
        }

        public static string GetDescription(this Enum val)
        {
            string enumName = val.ToString();
            var attrs = (DescriptionAttribute[])val.GetType().GetField(enumName).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attrs.Length > 0 ? attrs[0].Description : enumName;
        }

        public static string GetDescription(this object val)
        {
            if (val.GetType().IsEnum)
            {
                return ((Enum)val).GetDescription();
            }

            return null;
        }
    }
}
