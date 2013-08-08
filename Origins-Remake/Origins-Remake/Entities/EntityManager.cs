using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Entities.Mobs;
using Origins_Remake.Levels;
using Origins_Remake.Util;
using System;

namespace Origins_Remake.Entities
{
    public class EntityManager
    {
        static MainGame gameRef;
        static Player player;
        static Pathfinder pathfinder;
        static List<Enemy> enemies = new List<Enemy>();

        public static Player Player
        {
            get { return player; }
        }

        public static List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public EntityManager(Game game)
        {
            gameRef = (MainGame)game;
            player = new Player();

            pathfinder = new Pathfinder(LevelManager.CurrentLevel);

            enemies = new List<Enemy>();
            enemies.Add(new Skeleton(new Vector2(3) * 32));
        }

        public static void UpdateAll(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
        }

        public static void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public static void UpdateEnemies(GameTime gameTime)
        {
            enemies.ForEach(e => e.Update(gameTime));
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
        }

        public static double GetAngleToPlayer(Entity from)
        {
            return Math.Atan((EntityManager.Player.Position - from.Position).Y / (EntityManager.Player.Position - from.Position).X);
        }
    }
}
