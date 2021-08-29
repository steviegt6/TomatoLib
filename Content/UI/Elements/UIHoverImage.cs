#region License
// Copyright (C) 2021 TML team, TML contributors, Tomat, and TomatoLib Contributors
// MIT License according to tModLoader's License
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;

namespace TomatoLib.Content.UI.Elements
{
    public class UIHoverImage : UIImage
    {
        public string HoverText { get; }

        public UIHoverImage(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (!IsMouseHovering)
                return;

            Rectangle parentDims = Parent.GetDimensions().ToRectangle();
            parentDims.Y = 0;
            parentDims.Height = Main.screenHeight;

            UICommon.DrawHoverStringInBounds(spriteBatch, HoverText, parentDims);
        }
    }
}