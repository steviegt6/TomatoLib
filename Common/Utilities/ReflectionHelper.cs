using System.Reflection;

namespace TomatoLib.Common.Utilities
{
    /// <summary>
    ///     Provides simplistic reflection-related utilities.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///     Provides both publicity access modifier flags (<see cref="BindingFlags.Public"/> and <see cref="BindingFlags.NonPublic"/>).
        /// </summary>
        public static BindingFlags PublicityFlags => BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        ///     Provides both instance-related access flags (<see cref="BindingFlags.Instance"/> and <see cref="BindingFlags.Static"/>).
        /// </summary>
        public static BindingFlags InstancedFlags => BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        ///     Provides both <see cref="PublicityFlags"/> and <see cref="InstancedFlags"/>, allowing you to generally access most reflected types without issue.
        /// </summary>
        public static BindingFlags UniversalFlags => PublicityFlags | InstancedFlags;
    }
}