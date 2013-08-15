using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Util;
using OriginsLib.TileEngine;
using OriginsLib.Util;
using System;
using Origins_Remake.Entities;
using GameHelperLibrary.Shapes;

namespace Origins_Remake.Levels
{
    public class LevelManager
    {
        MainGame gameRef;

        static Dictionary<Guid, TileMap> levels = new Dictionary<Guid, TileMap>();
        static Dictionary<string, Guid> levelIds = new Dictionary<string, Guid>();

        static Texture2D red;

        public static Dictionary<string, Guid> LevelIds
        {
            get { return levelIds; }
        }

        public static Dictionary<Guid, TileMap> Levels
        {
            get { return levels; }
        }

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

            var tutLevel = new TileMap(0, 0);
            tutLevel.Load(@"test_level.slf");
            levels.Add(tutLevel.GUID, tutLevel);
            levelIds.Add("tutorial level", tutLevel.GUID);

            var connect_1Level = new TileMap(0, 0);
            connect_1Level.Load(@"connect_1.slf");
            levels.Add(connect_1Level.GUID, connect_1Level);
            levelIds.Add("connect 1 level", connect_1Level.GUID);

            var obstacle_1Level = new TileMap(0, 0);
            obstacle_1Level.Load(@"obstacle_1.slf");
            levels.Add(obstacle_1Level.GUID, obstacle_1Level);
            levelIds.Add("obstacle 1 level", obstacle_1Level.GUID);

            levels[levelIds["tutorial level"]].AddExitZone(new ExitZone(new Rectangle(tutLevel.WidthInTiles - 1, 3, 1, 2), levelIds["tutorial level"], levelIds["obstacle 1 level"]));
            levels[levelIds["obstacle 1 level"]].AddExitZone(new ExitZone(new Rectangle(0, 1, 1, 2), levelIds["obstacle 1 level"], levelIds["connect 1 level"]));

            ChangeLevel(levelIds["Tutorial Level".ToLower()]);

            red = new DrawableRectangle(gameRef.GraphicsDevice, new Vector2(10, 10), Color.White, true).Texture;
        }

        public static void Update(GameTime gameTime)
        {
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentLevel.Draw(batch, gameTime);

#if DEBUG
            currentLevel.exitZones.ForEach(zone => 
            { 
                batch.Draw(red, new Rectangle(zone.Bounds.X * Engine.TileWidth, zone.Bounds.Y * Engine.TileHeight, Engine.TileWidth * zone.Bounds.Width, Engine.TileHeight * zone.Bounds.Height), Color.Black * .3f); 
            });
#endif
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

        public static void ChangeLevel(Guid guid)
        {
            levels.TryGetValue(guid, out currentLevel);

            if (currentLevel.exitZones.Count > 0 && EntityManager.Player != null)
            {
                EntityManager.Player.Position = new Vector2(
                    currentLevel.ExitZones[0].Bounds.X * Engine.TileWidth,
                    currentLevel.ExitZones[0].Bounds.Y * Engine.TileHeight
                 );
            }
        }
    }
}
