﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<U> ConvertEnumerable<T, U>(this IEnumerable<T> originalEnumerable, Converter<T, U> converter)
        {
            foreach (T item in originalEnumerable)
            {
                yield return converter(item);
            }
        }
    }
}
