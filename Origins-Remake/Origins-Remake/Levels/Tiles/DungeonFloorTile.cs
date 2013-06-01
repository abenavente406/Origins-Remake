using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Util;

namespace Origins_Remake.Levels.Tiles
{
    class DungeonFloorTile : Tile
    {
        public DungeonFloorTile(Point pos)
            : base(pos, 32, 32)
        { }

        public override void SetTileSolid()
        {
            isSolid = false;
        }

        public override void SetTileTexture()
        {
            texture = SheetManager.TileSheets["dungeonTiles"].GetSubImage(2, 1);
        }
    }
}
