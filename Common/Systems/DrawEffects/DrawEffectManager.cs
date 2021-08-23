using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TomatoLib.Core.Drawing;

namespace TomatoLib.Common.Systems.DrawEffects
{
    /// <summary>
    ///     Manages <see cref="IDrawEffect"/> instances.
    /// </summary>
    public sealed class DrawEffectManager : SingletonSystem<DrawEffectManager>
    {
        public List<IDrawEffect> DrawEffects { get; }

        public DrawEffectManager()
        {
            DrawEffects = new List<IDrawEffect>();
        }

        public override void PostUpdateProjectiles()
        {
            // Update all IDrawEffect instances.
            foreach (IDrawEffect effect in DrawEffects)
                effect.Update();

            // Delete all IDrawEffect instances that were schedules for deletion.
            DrawEffects.RemoveAll(x => x.ScheduledForDeletion);
        }

        public override void PostDrawTiles()
        {
            // Draw all IDrawEffect instances

            SpriteBatchSnapshot snapshot = new(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);

            using DisposableSpriteBatch spriteBatch = new(Main.spriteBatch, snapshot, false);
            // Call PreDrawAll separately
            // as we want it to be ran before
            // actual instances are drawn
            foreach (IDrawEffect drawEffect in DrawEffects)
                drawEffect.PreDrawAll(spriteBatch.SpriteBatch);

            foreach (IDrawEffect drawEffect in DrawEffects.Where(drawEffect =>
                drawEffect.PreDraw(spriteBatch.SpriteBatch)))
            {
                drawEffect.Draw(spriteBatch.SpriteBatch);
                drawEffect.PostDraw(spriteBatch.SpriteBatch);
            }
        }
    }
}