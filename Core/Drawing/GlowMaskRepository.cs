using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;
using TomatoLib.Core.Utilities.Drawing;

// design based on the split mod's design lol
namespace TomatoLib.Core.Drawing
{
    /// <summary>
    ///     Basic implementation of <see cref="IGlowMaskRepository"/>.
    /// </summary>
    public class GlowMaskRepository : IGlowMaskRepository
    {
        public static IGlowMaskRepository Instance { get; set; }

        public readonly Dictionary<string, short> GlowMaskCollection;

        public GlowMaskRepository()
        {
            GlowMaskCollection = new Dictionary<string, short>();
        }

        public short GetGlowMask(string key)
        {
            if (GlowMaskCollection.TryGetValue(key, out short found))
                return found;

            return -1;
        }

        public short Register(Mod mod, string assetPath) => Register($"{mod.Name}/{assetPath}");

        public short Register(string assetPath)
        {
            short count = (short) TextureAssets.GlowMask.Length;
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(assetPath, AssetRequestMode.ImmediateLoad);
            texture.Value.Name = $"TomatoGlowMaskRepository_{texture.Name}";

            TextureAssets.GlowMask.Add(texture);
            GlowMaskCollection.Add(assetPath, count);
            return count;
        }

        public void RemoveGlowMasks()
        {
            TextureAssets.GlowMask = TextureAssets.GlowMask
                .Where(x => !x?.Name.StartsWith("TomatoGlowMaskRepository_") ?? true)
                .ToArray();

            GlowMaskCollection.Clear();
        }
    }
}