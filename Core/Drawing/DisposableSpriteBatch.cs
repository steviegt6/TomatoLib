#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace TomatoLib.Core.Drawing
{
    public class DisposableSpriteBatch : IDisposable
    {
        public readonly SpriteBatch SpriteBatch;

        protected readonly SpriteBatchSnapshot CachedSnapshot;
        protected readonly bool BeganPrior;

        // TODO: Is ref needed here?
        public DisposableSpriteBatch(SpriteBatch spriteBatch, SpriteBatchSnapshot snapshot, bool began)
        {
            if (began)
                spriteBatch.End();

            snapshot.BeginSpriteBatch(spriteBatch);

            CachedSnapshot = SpriteBatchSnapshot.FromSpriteBatch(spriteBatch);
            SpriteBatch = spriteBatch;
            BeganPrior = began;
        }

        public void Draw(Texture2D texture, Vector2 position, Color color) =>
            SpriteBatch.Draw(texture, position, color);

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color) =>
            SpriteBatch.Draw(texture, position, sourceRectangle, color);

        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale,
            effects, layerDepth);

        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale,
            effects, layerDepth);

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color) =>
            SpriteBatch.Draw(texture, destinationRectangle, color);

        public void Draw(
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color) => SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);

        public void Draw(
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation,
            origin, effects, layerDepth);

        public void DrawString(
            SpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color) => SpriteBatch.DrawString(spriteFont, text, position, color);

        public void DrawString(
            SpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            SpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color) =>
            SpriteBatch.DrawString(spriteFont, text, position, color);

        public void DrawString(
            SpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            SpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color) => SpriteBatch.DrawString(spriteFont, text, position, color);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color) => SpriteBatch.DrawString(spriteFont, text, position, color);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void DrawString(
            DynamicSpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) => SpriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale,
            effects, layerDepth);

        public void Dispose()
        {
            SpriteBatch.End();

            if (BeganPrior) 
                CachedSnapshot.BeginSpriteBatch(SpriteBatch);
        }
    }
}