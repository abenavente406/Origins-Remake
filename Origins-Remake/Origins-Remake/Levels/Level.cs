using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Levels.Objects;
using Origins_Remake.Util;

namespace Origins_Remake.Levels
{
    [Serializable]
    public abstract class Level
    {
        protected int realWidth;
        protected int realHeight;
        protected int tileWidth;
        protected int tileHeight;
        protected Tile[,] map;
        protected List<LevelObject> objects = new List<LevelObject>();

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
            objects.ForEach(o => { if (Camera.IsOnCamera(o.Bounds)) o.Update(gameTime); });
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            var cameraTilePos = Vector2Tile(Camera.Position);
            var cameraTilesSize = Vector2Tile(new Vector2(Camera.ViewPortBounds.Width, Camera.ViewPortBounds.Height));

            // I'm trying to draw the level to the screen only when necessary
            // --------------------------------------------------------------
            for (int x = cameraTilePos.X; x < cameraTilePos.X + (int)(cameraTilesSize.X / Camera.Zoom) + 2; x++)
            {
                if (x < 0 || x >= WidthInTiles) continue;
                for (int y = cameraTilePos.Y; y < cameraTilePos.Y + (int)(cameraTilesSize.Y / Camera.Zoom) + 2; y++)
                {
                    if (y < 0 || y >= HeightInTiles) continue;
                    batch.Draw(map[x, y].Texture, map[x, y].RealBounds, Color.White);
                }
            }

            objects.ForEach(o => o.Draw(batch, gameTime));
        }

        public Point Vector2Tile(Vector2 pos)
        {
            return new Point((int)pos.X / tileWidth, (int)pos.Y / tileHeight);
        }

        public Vector2 Tile2Vector(Point pos)
        {
            return new Vector2(pos.X * tileWidth, pos.Y * tileHeight);
        }
    }
}
