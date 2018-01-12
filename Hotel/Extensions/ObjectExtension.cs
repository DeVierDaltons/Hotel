using System;
using System.Collections.Generic;
using System.Linq;
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

        private static bool ArePropertiesEqual(this object thisObject, object other, string propName)
        {
            return GetPropertyValue(thisObject, propName) != GetPropertyValue(other, propName);
        }

        public static void CopyDelta<T>(this T target, T source) where T : class
        {
            if (target.GetType() != source.GetType())
                throw new ArgumentException(string.Format("target and source should be of same type (target:{0} source: {1})", target.GetType(), source.GetType()));

            foreach (var item in target.GetType().GetProperties())
            {
                if (ArePropertiesEqual(target, source, item.Name))
                {
                    var value = GetPropertyValue(source, item.Name);
                    target.GetType().GetProperty(item.Name).SetValue(target, value, null);
                }
            }
        }
    }
}
