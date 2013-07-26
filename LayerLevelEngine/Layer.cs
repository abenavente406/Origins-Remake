using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;

namespace LayerLevelEngine
{
    [Serializable]
    public class Layer
    {
        private int layerId;
        private Level parent;
        private float opacity = 1.0f;

        public float Opacity
        {
            get { return opacity; }
            set { opacity = MathHelper.Clamp(value, 0, 1); }
        }

        private Tile[,] tiles;

        public Layer(Level parent)
        {
            this.parent = parent;
            this.layerId = parent.LayerCount;

            tiles = new Tile[parent.WidthInTiles, parent.HeightInTiles];
        }

        public Layer(Level parent, Tile[,] tiles)
        {
            this.parent = parent;
            this.layerId = parent.LayerCount;
            this.tiles = tiles;
        }

        public void SetTile(int x, int y, Tile t)
        {
            tiles[x, y] = t;
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            int startX = (int)(Camera.Position.X / parent.TileWidth / Camera.Zoom) - 2;
            int startY = (int)(Camera.Position.Y / parent.TileHeight / Camera.Zoom) - 2;
            int endX = startX + (int)(Camera.ViewWidth / parent.TileWidth) + 4;
            int endY = startY + (int)(Camera.ViewHeight / parent.TileHeight) + 4;

            for (int x = startX; x < endX; x++)
            {
                if (x < 0 || x >= parent.WidthInTiles)
                    continue;
                for (int y = startY; y < endY; y++)
                {
                    if (y < 0 || y >= parent.HeightInTiles)
                        continue;

                    // TODO: Draw tiles properly
                    tiles[x, y].Draw(batch, gameTime, opacity);
                }
            }
        }

        public void LoadLayer()
        {
            for (int x = 0; x < parent.WidthInTiles; x++)
            {
                for (int y = 0; y < parent.HeightInTiles; y++)
                {
                    tiles[x, y].LoadTileSheet();
                }
            }
        }
    }
}
