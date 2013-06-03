using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;
using GameHelperLibrary;

namespace Origins_Remake.Entities.Mobs
{
    public class Skeleton : Enemy
    {
        public Skeleton(Vector2 pos)
            : base(pos)
        {
        }

        public override void SetAnimation()
        {
            var sheet = SheetManager.SpriteSheets["allEntities"];

            Texture2D[] imgsUp = new Texture2D[3] { sheet.GetSubImage(6, 3), sheet.GetSubImage(7, 3), sheet.GetSubImage(8, 3) };
            Texture2D[] imgsDown = new Texture2D[3] { sheet.GetSubImage(6, 0), sheet.GetSubImage(7, 0), sheet.GetSubImage(8, 0) };
            Texture2D[] imgsLeft = new Texture2D[3] { sheet.GetSubImage(6, 1), sheet.GetSubImage(7, 1), sheet.GetSubImage(8, 1) };
            Texture2D[] imgsRight = new Texture2D[3] { sheet.GetSubImage(6, 2), sheet.GetSubImage(7, 2), sheet.GetSubImage(8, 2) };

            animUp = new Animation(imgsUp);
            animDown = new Animation(imgsDown);
            animLeft = new Animation(imgsLeft);
            animRight = new Animation(imgsRight);
        }
    }
}
