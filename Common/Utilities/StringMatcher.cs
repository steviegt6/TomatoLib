using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TomatoLib.Common.Utilities
{
    /// <summary>
    ///     Object that can equate to and compare multiple strings.
    /// </summary>
    public class StringMatcher : IComparable<string>, IEquatable<string>, IEnumerable<string>, IEquatable<StringMatcher>
    {
        public virtual IEnumerable<string> Strings { get; }

        public StringMatcher(params string[] strings)
        {
            Strings = strings;
        }

        public int CompareTo(string other)
        {
            if (other is null || !Strings.Contains(other))
                return 1;

            string match = Strings.FirstOrDefault(x => x.Equals(other));

            return string.Compare(other, match, StringComparison.Ordinal);
        }

        public bool Equals(string other) => Strings.Any(x => x.Equals(other));

        public bool Equals(StringMatcher other) => other is not null && Strings.Any(x => other.Strings.Contains(x));

        public override bool Equals(object obj)
        {
            return obj switch
            {
                null => false,
                string str => Equals(str),
                StringMatcher matcher => Equals(matcher),
                _ => obj.GetHashCode() == GetHashCode()
            };
        }

        public override int GetHashCode() => Strings != null ? Strings.GetHashCode() : 0;

        public IEnumerator<string> GetEnumerator() => Strings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}