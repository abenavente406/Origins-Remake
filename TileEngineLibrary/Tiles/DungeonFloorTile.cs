using Microsoft.Xna.Framework;
using OriginsLibrary.Util;
using System;

namespace TileEngineLibrary.Tiles
{
    [Serializable]
    public class DungeonFloorTile : Tile
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
