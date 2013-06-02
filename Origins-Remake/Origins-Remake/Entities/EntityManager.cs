using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Entities.Mobs;

namespace Origins_Remake.Entities
{
    public class EntityManager
    {
        static MainGame gameRef;
        static Player player;

        public EntityManager(Game game)
        {
            gameRef = (MainGame)game;
            player = new Player();
        }

        public static void UpdateAll(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
        }

        public static void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            player.Draw(batch, gameTime);
        }
    }
}
