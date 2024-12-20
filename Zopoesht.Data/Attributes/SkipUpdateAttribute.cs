using System.Reflection;

namespace Zopoesht.Data.Attributes
{
    public class SkipUpdateAttribute : Attribute
    {
        public static bool IsDeclared(PropertyInfo propertyInfo)
            => propertyInfo.GetCustomAttribute(typeof(SkipUpdateAttribute)) != null;
    }
}
