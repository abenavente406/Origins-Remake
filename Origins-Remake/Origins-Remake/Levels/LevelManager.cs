using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Util;
using LayerLevelEngine;

namespace Origins_Remake.Levels
{
    public class LevelManager
    {
        MainGame gameRef;

        static Level currentLevel;

        public static Level CurrentLevel
        {
            get
            {
                if (currentLevel != null)
                    return currentLevel;
                else
                    return null;
            }
            set
            {
                currentLevel = value;
            }
        }

        public LevelManager(Game game)
        {
            gameRef = (MainGame)game;

            currentLevel = new Level(@"D:\level.slf");
        }

        public static void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentLevel.Draw(batch, gameTime);
        }

        public static bool IsWallTile(Vector2 pos)
        {
            var point = Vector2Tile(pos);
            return IsWallTile(point);            
        }

        public static bool IsWallTile(Point point)
        {
            return false;
        }

        public static bool IsWallTile(float x, float y, int width, int height)
        {
            return false;
        }

        public static bool IsTileBlocked(int tx, int ty)
        {
            return false;
        }

        public static Point Vector2Tile(Vector2 pos)
        {
            return new Point((int)pos.X / currentLevel.TileWidth, (int)pos.Y / currentLevel.TileHeight);
        }

        public static Vector2 Tile2Vector(Point pos)
        {
            return new Vector2(pos.X * currentLevel.TileWidth, pos.Y * currentLevel.TileHeight);
        }

        public static Vector2 GetCenterOfTile(Point tile)
        {
            var pos = Tile2Vector(tile);
            pos += new Vector2(currentLevel.TileWidth / 2, currentLevel.TileHeight / 2);
            return pos;
        }
    }
}
