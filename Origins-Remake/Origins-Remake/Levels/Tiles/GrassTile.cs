using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Util;

namespace Origins_Remake.Levels.Tiles
{
    class GrassTile : Tile
    {
        public GrassTile(Point pos)
            : base(pos, 32, 32) { }

        public override void SetTileSolid()
        {
            isSolid = false;
        }

        public override void SetTileTexture()
        {
            texture = SheetManager.TileSheets["basicTiles"].GetSubImage(1, 0);
        }
    }
}
