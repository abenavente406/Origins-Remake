using Microsoft.Xna.Framework;
using OriginsLibrary.Util;
using System;

namespace TileEngineLibrary.Tiles
{
    [Serializable]
    public class BlackTile : Tile
    {
        public BlackTile(Point pos)
            : base(pos, 32, 32) { }

        public override void SetTileSolid()
        {
            isSolid = true;
        }

        public override void SetTileTexture()
        {
            texture = SheetManager.TileSheets["basicTiles"].GetSubImage(0, 0);
        }
    }
}
