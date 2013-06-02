using Microsoft.Xna.Framework;
using OriginsLibrary.Util;
using System;

namespace TileEngineLibrary.Tiles
{
        [Serializable]
    public class DungeonWallTile : Tile
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
