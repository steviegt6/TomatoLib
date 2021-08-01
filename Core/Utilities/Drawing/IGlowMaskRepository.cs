namespace TomatoLib.Core.Utilities.Drawing
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