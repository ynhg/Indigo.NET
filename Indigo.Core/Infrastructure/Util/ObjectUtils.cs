using System;
using System.Reflection;

namespace Indigo.Infrastructure.Util
{
    public static class ObjectUtils
    {
        public static void CopyProperties(object src, object dest)
        {
            Type destType = dest.GetType();
            foreach (PropertyInfo srcProp in src.GetType().GetProperties())
            {
                if (srcProp.CanRead)
                {
                    PropertyInfo destProp = destType.GetProperty(srcProp.Name);
                    if (destProp != null && destProp.CanWrite)
                    {
                        destProp.SetValue(dest, srcProp.GetValue(src, null), null);
                    }
                }
            }
        }

        public new static bool Equals(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;

            return obj1 != null && obj1.Equals(obj2);
        }
    }
}