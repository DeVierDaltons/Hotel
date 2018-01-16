using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Extensions
{
    public static class ObjectExtension 
    {
        private static object GetPropertyValue(this object thisObject, string propertyName)
        {
            return thisObject.GetType().GetProperty(propertyName).GetValue(thisObject, null);
        }

        public static void CopyDeltaProperties(this object target, object source)
        {
            Type TargetType = target.GetType();
            if (TargetType != source.GetType())
                throw new ArgumentException(string.Format("target and source should be of same type (target:{0} source: {1})", TargetType, source.GetType()));

            foreach (PropertyInfo propertyInfo in TargetType.GetProperties())
            {
                var sourceValue = propertyInfo.GetValue(source);
                var targetValue = propertyInfo.GetValue(target);
                if (sourceValue != targetValue)
                {
                    propertyInfo.SetValue(target, sourceValue, null);
                }
            }
        }

        public static bool ValueOccursInProperties<T>(this T target, object value) where T : class
        {
            foreach (var item in target.GetType().GetProperties())
            {
                if(GetPropertyValue(target, item.Name) == value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
