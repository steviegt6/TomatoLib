using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TomatoLib.Common.Utilities.Extensions;

namespace TomatoLib.Common.Assets
{
    /// <summary>
    ///     Holds an <see cref="Asset{T}"/>-wrapped <see cref="Texture2D"/> asset as well was a two-dimensional <see cref="Color"/> array.
    /// </summary>
    /// <remarks>
    ///     You must be careful when creating a new noise instance, as <see cref="Texture2DHelpers.GetColors"/> is used. <br />
    ///     This will break if the instance is not created on the main thread. Thanks, FNA.
    /// </remarks>
    public struct Noise
    {
        /// <summary>
        ///     The noise's texture, wrapped in an <see cref="Asset{T}"/>.
        /// </summary>
        public Asset<Texture2D> Texture { get; }

        /// <summary>
        ///     A two-dimensional array of <see cref="Color"/><c>s</c> retrieved from <see cref="Texture2DHelpers.GetColors"/>.
        /// </summary>
        public Color[,] NoiseData { get; private set; }

        /// <summary>
        ///     Initializes a new instance.
        /// </summary>
        public Noise(Asset<Texture2D> texture)
        {
            Texture = texture;
            NoiseData = texture.Value.GetColors();
        }

        /// <summary>
        ///     Refreshes the value of <see cref="NoiseData"/> by calling <see cref="Texture2DHelpers.GetColors"/>.
        /// </summary>
        /// <returns><see cref="NoiseData"/>'s new value.</returns>
        public Color[,] RefreshNoiseData() => NoiseData = Texture.Value.GetColors();
    }
}