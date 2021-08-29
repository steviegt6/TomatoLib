#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TomatoLib.Common.Systems.DrawEffects
{
    public abstract class BaseDrawEffect : Entity, IDrawEffect
    {
        public Action Destroy { get; }

        public bool ScheduledForDeletion { get; set; }

        protected BaseDrawEffect()
        {
            Destroy += () => ScheduledForDeletion = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Update() => position += velocity;
    }
}