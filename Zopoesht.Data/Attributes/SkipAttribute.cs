using System.Reflection;

namespace Zopoesht.Data.Attributes
{
    public class SkipAttribute : Attribute
    {
        public static bool IsDeclared(PropertyInfo propertyInfo)
             => propertyInfo.GetCustomAttribute(typeof(SkipAttribute)) != null;
    }
}
