#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Collections.Generic;
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
            SpriteBatchSnapshot snapshot = new(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                Main.GameViewMatrix.ZoomMatrix);

            using DisposableSpriteBatch spriteBatch = new(Main.spriteBatch, snapshot, false);

            foreach (IDrawEffect drawEffect in DrawEffects) 
                drawEffect.Draw(spriteBatch.SpriteBatch);
        }
    }
}