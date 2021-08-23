using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TomatoLib.Common.Systems.DrawEffects
{
    public abstract class BaseDrawEffect : Entity, IDrawEffect
    {
        public virtual float Scale { get; set; } = 1f;

        public virtual float[] SyncedData { get; set; } = new float[3];

        public Action Destroy { get; }

        public bool ScheduledForDeletion { get; set; }

        protected BaseDrawEffect()
        {
            Destroy += () => ScheduledForDeletion = true;
        }

        public virtual void PreDrawAll(SpriteBatch spriteBatch)
        {
        }

        public virtual bool PreDraw(SpriteBatch spriteBatch) => true;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void PostDraw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Update() => position += velocity;
    }
}