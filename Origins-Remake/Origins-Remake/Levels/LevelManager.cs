using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Util;
using OriginsLib.TileEngine;
using OriginsLib.Util;

namespace Origins_Remake.Levels
{
    public class LevelManager
    {
        MainGame gameRef;

        static TileMap currentLevel;

        public static TileMap CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
            }
        }

        public LevelManager(Game game)
        {
            gameRef = (MainGame)game;

            currentLevel = new TileMap(0, 0);
            currentLevel.Load(@"D:\test_level.slf");
        }

        public static void Update(GameTime gameTime)
        {
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentLevel.Draw(batch, gameTime);
        }

        public static bool IsWallTile(Vector2 pos)
        {
            return false;
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

        public static Point Vector2Cell(Vector2 pos)
        {
            return Engine.VectorToCell(pos);
        }

        public static Vector2 Tile2Vector(Point pos)
        {
            return new Vector2(pos.X * Engine.TileWidth, pos.Y * Engine.TileHeight);
        }

        public static Vector2 GetCenterOfTile(Point tile)
        {
            var pos = Tile2Vector(tile);
            pos += new Vector2(Engine.TileWidth / 2, Engine.TileHeight);
            return pos;
        }
    }
}
