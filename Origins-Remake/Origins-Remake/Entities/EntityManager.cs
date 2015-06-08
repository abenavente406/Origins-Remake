using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Entities.Mobs;
using Origins_Remake.Levels;
using Origins_Remake.Util;
using System;
using OriginsLib.Util;
using OriginsLib.TileEngine;

namespace Origins_Remake.Entities
{
    public class EntityManager
    {
        static MainGame gameRef;
        static Player player;
        static Pathfinder pathfinder;
        static List<Enemy> enemies = new List<Enemy>();
        static List<Npc> npcs = new List<Npc>();

        static string[] alphabet = new string[] 
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
            "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " ", " ", 
            " "
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
            player.Position = new Vector2(1, 5 * Engine.TileHeight);

            pathfinder = new Pathfinder(LevelManager.CurrentLevel);

            enemies = new List<Enemy>();

#if DEBUG
            enemies.Add(new Skeleton(new Vector2(3) * 32));
#endif

            Random rand = new Random();

            npcs = new List<Npc>();

            var sheet = SheetManager.SpriteSheets["allEntities"];

            var questGiver1 = new QuestGiver(new Vector2(0, 5 * Engine.TileHeight));
            string d = "Hi there, " + Config.currentlyPlaying + "!" +
                " I see you've discovered the first tutorial npc in the game.  Well, I'm here to teach you how to play." + 
                " To interact with the world, press 'Z'.  To exit our dialogue boxes, press 'X'.  To pause, press 'Esc'." + 
                " To fight?  Just click away!  Good luck to you. And I hope to see you around!!  Goodbye!";
#if DEBUG
            d += "  P.S. - Don't mind that guy following you.  That must means we are being run in DEBUG mode.";
#endif 

            questGiver1.AddDialogue(d);
            questGiver1.OnFirstTimeSpokenTo += (delegate(object sender, EventArgs args)
            {
                questGiver1.Dialogues[0] = "I already spoke to you! Try pressing 'P'";
            });

            questGiver1.SetTextures(
                down: sheet.GetSubImage(0, 28),
                left: sheet.GetSubImage(0, 28),
                right: sheet.GetSubImage(0, 28),
                up: sheet.GetSubImage(0, 28)
            );
            questGiver1.Name = "Barney";
            npcs.Add(questGiver1);

            for (int i = 0; i < 5; i++)
            {
                var n = new Npc(new Vector2(6) * 32 * (i + 1), rand);
                n.AddDialogue("Howdy partner!");
                n.AddDialogue("What brings you to this part of town son?");
                n.AddDialogue("How you doin' fella?");
                n.AddDialogue("Have I seen you around before?");

                var spriteIndex = rand.Next(4);
                n.SetTextures(
                    down:sheet.GetSubImage(spriteIndex, 28),
                    left: sheet.GetSubImage(spriteIndex, 28),
                    right: sheet.GetSubImage(spriteIndex, 28),
                    up: sheet.GetSubImage(spriteIndex, 28)
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
            npcs.ForEach(n =>
                {
                    n.Draw(batch, gameTime);
                });

            player.Draw(batch, gameTime);

            enemies.ForEach(e =>
            {
                e.Draw(batch, gameTime);

#if DEBUG
                if (e.aiState == AiState.TARGETTING)
                    e.DrawPathToPlayer(batch, Player);
#endif
            });

        }

        public static double GetAngleToPlayer(Entity from)
        {
            return Math.Atan((EntityManager.Player.Position - from.Position).Y / (EntityManager.Player.Position - from.Position).X);
        }
    }
}
