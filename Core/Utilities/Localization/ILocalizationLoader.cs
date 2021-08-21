using System.Collections.Generic;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities;

namespace TomatoLib.Core.Utilities.Localization
{
    /// <summary>
    ///     Provides an implementable localization loader for a mod.
    /// </summary>
    /// <remarks>
    ///     Does *not* auto-load.
    /// </remarks>
    [Autoload(false)]
    public interface ILocalizationLoader : ILoadable
    {
        public Dictionary<StringMatcher, ILocalizationFileParser> ExtensionsToParsers { get; }
    }
}