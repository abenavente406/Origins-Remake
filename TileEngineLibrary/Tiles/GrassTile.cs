using Microsoft.Xna.Framework;
using OriginsLibrary.Util;
using System;

namespace TileEngineLibrary.Tiles
{
    [Serializable]
    public class GrassTile : Tile
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
