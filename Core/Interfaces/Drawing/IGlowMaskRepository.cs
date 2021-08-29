#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

namespace TomatoLib.Core.Interfaces.Drawing
{
    /// <summary>
    ///     Simple glow-mask provider.
    /// </summary>
    public interface IGlowMaskRepository
    {
        /// <summary>
        ///     Gets the indexed value of a glow-mask given the key.
        /// </summary>
        short GetGlowMask(string key);

        /// <summary>
        ///     Unloads added glow-masks.
        /// </summary>
        void RemoveGlowMasks();
    }
}