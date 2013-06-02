using Microsoft.Xna.Framework;
using OriginsLibrary.Util;

namespace TileEngineLibrary.Tiles
{
    public class SandTile : Tile
    {
        public SandTile(Point pos)
            : base(pos, 32, 32) { }

        public override void SetTileSolid()
        {
            isSolid = false;
        }

        public override void SetTileTexture()
        {
            texture = SheetManager.TileSheets["basicTiles"].GetSubImage(4, 0);
        }
    }
}
