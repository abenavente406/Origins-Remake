using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;
using System;

namespace TileEngineLibrary.Tiles
{

    [Serializable]
    public class WaterTile : AnimatedTile
    {
        public WaterTile(Point pos)
            : base(pos, 32, 32)
        {
        }

        public override void SetTileSolid()
        {
            isSolid = true;
        }

        public override void SetTileAnimation()
        {
            var texture1 = SheetManager.TileSheets["basicTiles"].GetSubImage(3, 0);
            var texture2 = SheetManager.TileSheets["basicTiles"].GetSubImage(2, 0);
            var texture3 = SheetManager.TileSheets["basicTiles"].GetSubImage(2, 1);

            animation = new Animation(new Texture2D[3] { texture1, texture2, texture3 }, 500f);
            texture = texture1;
        }
    }
}
