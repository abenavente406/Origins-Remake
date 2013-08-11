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
            currentLevel.Load(@"test_level.slf");
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
            return IsTileBlocked((int)pos.X, (int)pos.Y);
        }

        public static bool IsWallTile(Point point)
        {
            return IsTileBlocked(point.X, point.Y);
        }

        public static bool IsWallTile(float x, float y, int width, int height)
        {
            int atx1 = (int)MathHelper.Clamp((x) / Engine.TileWidth, 0, currentLevel.WidthInTiles - 1);
            int atx2 = (int)MathHelper.Clamp((x + width) / Engine.TileWidth, 0, currentLevel.WidthInTiles - 1);
            int aty1 = (int)MathHelper.Clamp((y + height / 2) / Engine.TileHeight, 0, currentLevel.HeightInTiles - 1);
            int aty2 = (int)MathHelper.Clamp((y + height) / Engine.TileHeight, 0, currentLevel.HeightInTiles - 1);

            if (IsTileBlocked(atx1, aty1))
                return true;
            if (IsTileBlocked(atx1, aty2))
                return true;
            if (IsTileBlocked(atx2, aty1))
                return true;
            if (IsTileBlocked(atx2, aty2))
                return true;

            return false;
        }

        public static bool IsTileBlocked(int tx, int ty)
        {
            return currentLevel.GetIsCollision(tx, ty);
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
