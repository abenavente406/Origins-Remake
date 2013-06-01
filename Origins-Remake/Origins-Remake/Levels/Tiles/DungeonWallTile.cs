using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Util;

namespace Origins_Remake.Levels.Tiles
{
    class DungeonWallTile : Tile
    {
        public DungeonWallTile(Point pos)
            : base(pos, 32, 32)
        { }

        public override void SetTileSolid()
        {
            isSolid = true;
        }

        public override void SetTileTexture()
        {
            texture = SheetManager.TileSheets["dungeonTiles"].GetSubImage(1, 1);
        }
    }
}
