#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using Microsoft.Xna.Framework.Graphics;

namespace TomatoLib.Common.Systems.DrawEffects
{
    /// <summary>
    ///     Simple base for a effect drawing.
    /// </summary>
    public interface IDrawEffect
    {
        /// <summary>
        ///     Action called when the effect is destroyed. This should be used to "kill" the effect instance.
        /// </summary>
        Action Destroy { get; }

        /// <summary>
        ///     Whether this effect is schedules to get deleted after an <see cref="Update"/> enumeration operation.
        /// </summary>
        bool ScheduledForDeletion { get; }

        // self-explanatory
        /// <summary>
        /// </summary>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        ///     Called once per frame when this effect is updated. Useful for AI shenanigans and the like.
        /// </summary>
        void Update();
    }
}