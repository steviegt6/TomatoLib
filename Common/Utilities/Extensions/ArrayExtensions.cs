#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
using System.Linq;

namespace TomatoLib.Common.Utilities.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Adds an element to the end of an array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="array">The instance to the array to add to.</param>
        /// <param name="item">The element to be added.</param>
        /// <returns>The array with the added element.</returns>
        /// <remarks>
        ///     Adding to array should generally be avoided. This converts the entire array to a list, adds the element, and converts to back to an array. <br />
        ///     Needless to say, it isn't necessarily efficient.
        /// </remarks>
        public static T[] Add<T>(this T[] array, T item)
        {
            List<T> list = array.ToList();
            list.Add(item);
            return list.ToArray();
        }
    }
}