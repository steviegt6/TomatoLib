#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomatoLib.Common.Utilities.Extensions;

namespace TomatoLib.Core.Drawing
{
    public readonly struct SpriteBatchSnapshot
    {
        public SpriteSortMode SortMode { get; }

        public BlendState BlendState { get; }

        public SamplerState SamplerState { get; }

        public DepthStencilState DepthStencilState { get; }

        public RasterizerState RasterizerState { get; }

        public Effect Effect { get; }

        public Matrix TransformMatrix { get; }

        public SpriteBatchSnapshot(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            SortMode = sortMode;
            BlendState = blendState;
            SamplerState = samplerState;
            DepthStencilState = depthStencilState;
            RasterizerState = rasterizerState;
            Effect = effect;
            TransformMatrix = transformMatrix;
        }

        public void BeginSpriteBatch(SpriteBatch spriteBatch) => spriteBatch.Begin(SortMode, BlendState,
            SamplerState, DepthStencilState, RasterizerState, Effect, TransformMatrix);

        public static SpriteBatchSnapshot FromSpriteBatch(SpriteBatch spriteBatch)
        {
            SpriteSortMode sortMode = spriteBatch.GetFieldValue<SpriteBatch, SpriteSortMode>("sortMode");
            BlendState blendState = spriteBatch.GetFieldValue<SpriteBatch, BlendState>("blendState");
            SamplerState samplerState = spriteBatch.GetFieldValue<SpriteBatch, SamplerState>("samplerState");
            DepthStencilState depthStencilState = spriteBatch.GetFieldValue<SpriteBatch, DepthStencilState>("depthStencilState");
            RasterizerState rasterizerState = spriteBatch.GetFieldValue<SpriteBatch, RasterizerState>("rasterizerState");
            Effect effect = spriteBatch.GetFieldValue<SpriteBatch, Effect>("customEffect");
            Matrix transformMatrix = spriteBatch.GetFieldValue<SpriteBatch, Matrix>("transformMatrix");

            return new SpriteBatchSnapshot(sortMode, blendState, samplerState, 
                depthStencilState, rasterizerState, effect, transformMatrix);
        }
    }
}