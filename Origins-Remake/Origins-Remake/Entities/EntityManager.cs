using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Entities.Mobs;
using Origins_Remake.Levels;
using Origins_Remake.Util;
using System;
using OriginsLib.Util;

namespace Origins_Remake.Entities
{
    public class EntityManager
    {
        static MainGame gameRef;
        static Player player;
        static Pathfinder pathfinder;
        static List<Enemy> enemies = new List<Enemy>();
        static List<Npc> npcs = new List<Npc>();

        static string[] alphabet = new string[26] 
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
            "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };

        public static Player Player
        {
            get { return player; }
        }

        public static List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public static List<Npc> Npcs
        {
            get { return npcs; }
        }

        public EntityManager(Game game)
        {
            gameRef = (MainGame)game;
            player = new Player();

            pathfinder = new Pathfinder(LevelManager.CurrentLevel);

            enemies = new List<Enemy>();
            enemies.Add(new Skeleton(new Vector2(3) * 32));

            Random rand = new Random();

            npcs = new List<Npc>();
            for (int i = 0; i < 10; i++)
            {
                var n = new Npc(new Vector2(6) * 32 * i, rand);
                n.AddDialogue("Howdy partner!");
                n.AddDialogue("What brings you to this part of town son?");
                n.AddDialogue("How you doin' fella?");
                n.AddDialogue("Have I seen you around before?");

                var sheet = SheetManager.SpriteSheets["allEntities"];
                n.SetTextures(
                    down:sheet.GetSubImage(7, 0),
                    left:sheet.GetSubImage(7, 1),
                    right:sheet.GetSubImage(7, 2),
                    up:sheet.GetSubImage(7, 3)
                );
                npcs.Add(n);
            }
        }

        public static void UpdateAll(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
            UpdateNpcs(gameTime);
        }

        public static void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public static void UpdateEnemies(GameTime gameTime)
        {
            enemies.ForEach(e => e.Update(gameTime));
        }

        public static void UpdateNpcs(GameTime gameTime)
        {
            npcs.ForEach(n => n.Update(gameTime));
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            player.Draw(batch, gameTime);

            enemies.ForEach(e =>
                {
                    e.Draw(batch, gameTime);

#if DEBUG
                    if (e.aiState == AiState.TARGETTING)
                        e.DrawPathToPlayer(batch, Player);
#endif
                });

            npcs.ForEach(n =>
                {
                    n.Draw(batch, gameTime);
                });
        }

        public static double GetAngleToPlayer(Entity from)
        {
            return Math.Atan((EntityManager.Player.Position - from.Position).Y / (EntityManager.Player.Position - from.Position).X);
        }
    }
}
