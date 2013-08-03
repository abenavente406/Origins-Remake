using System.Collections.Generic;
using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OriginsLib.Util
{
    public static class SheetManager
    {
        static Game gameRef;

        static Dictionary<string, SpriteSheet> spriteSheets = new Dictionary<string, SpriteSheet>();
        static Dictionary<string, SpriteSheet> tileSheets = new Dictionary<string,SpriteSheet>();

        public static Dictionary<string, SpriteSheet> SpriteSheets
        {
            get { return spriteSheets; }
        }

        public static Dictionary<string, SpriteSheet> TileSheets
        {
            get { return tileSheets; }
        }

        public static void Initialize(Game game)
        {
            gameRef = game;

            AddSpriteSheet("allEntities", new SpriteSheet(gameRef.Content.Load<Texture2D>("Sprites\\Entities\\all_entities"), 32, 32, gameRef.GraphicsDevice));
            AddSpriteSheet("player", new SpriteSheet(gameRef.Content.Load<Texture2D>("Sprites\\Entities\\player"), 32, 32, gameRef.GraphicsDevice));
            AddTileSheet("basicTiles", new SpriteSheet(gameRef.Content.Load<Texture2D>("Sprites\\tilesheet_0"), 32, 32, gameRef.GraphicsDevice));
            AddTileSheet("dungeonTiles", new SpriteSheet(gameRef.Content.Load<Texture2D>("Sprites\\dungeon_tiles"), 32, 32, gameRef.GraphicsDevice));
        }

        public static void AddTileSheet(string name, SpriteSheet sheet)
        {
            tileSheets.Add(name, sheet);
        }

        public static void AddSpriteSheet(string name, SpriteSheet sheet)
        {
            spriteSheets.Add(name, sheet);
        }

        public static void AddSpriteSheet(string name, string path, int spriteWidth, int spriteHeight)
        {
            spriteSheets.Add(name, new SpriteSheet(path, spriteWidth, spriteHeight, gameRef.GraphicsDevice));
        }

        public static void AddTileSheet(string name, string path, int spriteWidth, int spriteHeight)
        {
            tileSheets.Add(name, new SpriteSheet(path, spriteWidth, spriteHeight, gameRef.GraphicsDevice));
        }
    }
}
