using GameHelperLibrary;
using Microsoft.Xna.Framework;
using System;

namespace TileEngineLibrary.Tiles
{
    [Serializable]
    public class AnimatedTile : Tile
    {
        [NonSerialized]
        protected Animation animation;

        public AnimatedTile(Point pos, int width, int height)
            : base(pos, width, height)
        {
            SetTileAnimation();
        }

        public override void SetTileSolid()
        {
            return;
        }

        public override void SetTileTexture()
        {
            return;
        }

        public virtual void SetTileAnimation()
        {
            return;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, GameTime gameTime)
        {
            animation.Draw(batch, gameTime, RealBounds);
        }
    }
}
