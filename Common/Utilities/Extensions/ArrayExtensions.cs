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
        public static T[] Add<T>(this T[] array, T item)
        {
            List<T> list = array.ToList();
            list.Add(item);
            return list.ToArray();
        }
    }
}