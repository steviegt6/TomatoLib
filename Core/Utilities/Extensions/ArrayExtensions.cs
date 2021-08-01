using System.Collections.Generic;
using System.Linq;

namespace TomatoLib.Core.Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Add<T>(this T[] array, T item)
        {
            List<T> list = array.ToList();
            list.Add(item);
            return list.ToArray();
        }
    }
}