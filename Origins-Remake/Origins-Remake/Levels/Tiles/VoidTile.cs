using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Origins_Remake.Levels.Tiles
{
    public class VoidTile : Tile
    {
        public VoidTile(Point pos)
            : base(pos, 32, 32) { }

        public override void SetId()
        {
            id = 0;
        }

        public override void SetTileSolid()
        {
            isSolid = true;
        }

        public override void SetTileTexture()
        {
            
        }
    }
}
