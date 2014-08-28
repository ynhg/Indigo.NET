namespace Indigo.Infrastructure.Util
{
    public static class ObjectUtils
    {
        public static void CopyProperties(object src, object dest)
        {
            var destType = dest.GetType();
            foreach (var srcProp in src.GetType().GetProperties())
            {
                if (srcProp.CanRead)
                {
                    var destProp = destType.GetProperty(srcProp.Name);
                    if (destProp != null && destProp.CanWrite)
                    {
                        destProp.SetValue(dest, srcProp.GetValue(src, null), null);
                    }
                }
            }
        }

        public new static bool Equals(object obj1, object obj2)
        {
            if (object.ReferenceEquals(obj1, obj2)) return true;

            return obj1 != null ? obj1.Equals(obj2) : false;
        }
    }
}
