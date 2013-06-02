using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngineLibrary
{
    [Serializable]
    public abstract class Level
    {
        public int realWidth;
        public int realHeight;
        public int tileWidth;
        public int tileHeight;
        public Tile[,] map;

        public int RealWidth
        {
            get { return realWidth; }
        }

        public int RealHeight
        {
            get { return realHeight; }
        }

        public int WidthInTiles
        {
            get { return realWidth / tileWidth; }
        }

        public int HeightInTiles
        {
            get { return realHeight / tileHeight; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
        }

        public Tile[,] Map
        {
            get { return map; }
        }

        public Level(int width, int height, int tileWidth, int tileHeight)
        {
            realWidth = width * tileWidth;
            realHeight = height * tileHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            map = new Tile[WidthInTiles, HeightInTiles];
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime, Rectangle view)
        {
            var cameraTilePos = Vector2Tile(new Vector2(view.X, view.Y));
            var cameraTilesSize = Vector2Tile(new Vector2(view.Width, view.Height));

            // I'm trying to draw the level to the screen only when necessary
            // --------------------------------------------------------------
            for (int x = cameraTilePos.X; x < cameraTilePos.X + cameraTilesSize.X + 2; x++)
            {
                if (x < 0 || x >= WidthInTiles) continue;
                for (int y = cameraTilePos.Y; y < cameraTilePos.Y + cameraTilesSize.Y + 2; y++)
                {
                    if (y < 0 || y >= HeightInTiles) continue;
                    batch.Draw(map[x, y].Texture, map[x, y].RealBounds, Color.White);
                }
            }
        }

        protected Point Vector2Tile(Vector2 pos)
        {
            return new Point((int)pos.X / tileWidth, (int)pos.Y / tileHeight);
        }

        protected Vector2 Tile2Vector(Point pos)
        {
            return new Vector2(pos.X * tileWidth, pos.Y * tileHeight);
        }
    }
}
