using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Util;

namespace Origins_Remake.Levels.Objects
{
    public class GrassShrub : LevelObject
    {
        public GrassShrub(Vector2 pos)
        {
            this.pos = pos;
        }

        public override void SetTexture()
        {
            texture = SheetManager.TileSheets["basicTiles"].GetSubImage(1, 1);
        }

        public override void SetSolid()
        {
            isSolid = false;
        }
    }
}
