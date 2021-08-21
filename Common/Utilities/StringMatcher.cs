using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TomatoLib.Common.Utilities
{
    /// <summary>
    ///     Object that can equate and compare to multiple strings.
    /// </summary>
    public class StringMatcher : IComparable<string>, IEquatable<string>, IEnumerable<string>
    {
        public virtual IEnumerable<string> Strings { get; }

        public StringMatcher(params string[] strings)
        {
            Strings = strings;
        }

        public int CompareTo(string other)
        {
            /*
             *         public int CompareTo(object? value)
        {
            if (value == null)
            {
                return 1;
            }

            if (!(value is string other))
            {
                throw new ArgumentException(SR.Arg_MustBeString);
            }

            return CompareTo(other); // will call the string-based overload
        }
             */
            if (other is null || !Strings.Contains(other))
                return 1;

            string match = Strings.FirstOrDefault(x => x.Equals(other));

            return string.Compare(other, match, StringComparison.Ordinal);
        }

        public bool Equals(string other) => Strings.Any(x => x.Equals(other));

        public IEnumerator<string> GetEnumerator() => Strings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}