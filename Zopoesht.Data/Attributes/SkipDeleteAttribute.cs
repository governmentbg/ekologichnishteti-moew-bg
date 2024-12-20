using System.Reflection;

namespace Zopoesht.Data.Attributes
{
    public class SkipDeleteAttribute: Attribute
    {
        public static bool IsDeclared(PropertyInfo propertyInfo)
            => propertyInfo.GetCustomAttribute(typeof(SkipDeleteAttribute)) != null;
    }
}
