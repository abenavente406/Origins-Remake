using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TileEngineLibrary;
using Origins_Remake.Util;

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

            currentLevel = new SlfLevel("C:\\Users\\Anthony Benavente\\Desktop\\test.slf");
        }

        public static void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentLevel.Draw(batch, gameTime, Camera.ViewPortBounds);
        }

        public static bool IsWallTile(Vector2 pos)
        {
            var point = Vector2Tile(pos);
            return IsWallTile(point);            
        }

        public static bool IsWallTile(Point point)
        {
            if (point.X < 0 || point.X >= currentLevel.WidthInTiles ||
                point.Y < 0 || point.Y >= currentLevel.HeightInTiles)
                return true;
            else
                return currentLevel.Map[point.X, point.Y].IsSolid;
        }

        public static bool IsWallTile(float x, float y, int width, int height)
        {
            int atx1 = (int)MathHelper.Clamp((x) / currentLevel.TileWidth, 0, currentLevel.WidthInTiles - 1);
            int atx2 = (int)MathHelper.Clamp((x + width) / currentLevel.TileWidth, 0, currentLevel.WidthInTiles - 1);
            int aty1 = (int)MathHelper.Clamp((y + height / 2) / currentLevel.TileHeight, 0, currentLevel.HeightInTiles - 1);
            int aty2 = (int)MathHelper.Clamp((y + height) / currentLevel.TileHeight, 0, currentLevel.HeightInTiles - 1);

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
            return currentLevel.Map[tx, ty].IsSolid;
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
            pos += new Vector2(currentLevel.tileWidth / 2, currentLevel.tileHeight / 2);
            return pos;
        }
    }
}
