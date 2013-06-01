using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Levels.Objects
{
    public class Tree : LevelObject
    {
        public Tree(Vector2 pos)
        {
            this.pos = pos;
        }

        public override void SetTexture()
        {
            texture = MainGame.gameContent.Load<Texture2D>("Sprites\\tree");
            width = texture.Width;
            height = texture.Height;
        }

        public override void SetSolid()
        {
            isSolid = true;
        }
    }
}
