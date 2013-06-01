using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Util
{
    public static class SheetManager
    {
        static MainGame gameRef;
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
            gameRef = (MainGame)game;

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
    }
}
