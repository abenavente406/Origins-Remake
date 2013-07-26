using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Origins_Remake.Util;
using OriginsLibrary.Util;

namespace Origins_Remake.Entities.Mobs
{
    public class Player : AnimatedEntity
    {
        public Player()
            : base(Vector2.Zero)
        {
            this.pos = new Vector2(32, 32);

            width = 28;
            height = 30;
        }

        public override void SetAnimation()
        {
            var sheet = SheetManager.SpriteSheets["player"];

            Texture2D[] imgsUp = new Texture2D[3] { sheet.GetSubImage(0, 3), sheet.GetSubImage(1, 3), sheet.GetSubImage(2, 3) };
            Texture2D[] imgsDown = new Texture2D[3] { sheet.GetSubImage(0, 0), sheet.GetSubImage(1, 0), sheet.GetSubImage(2, 0) };
            Texture2D[] imgsLeft = new Texture2D[3] { sheet.GetSubImage(0, 1), sheet.GetSubImage(1, 1), sheet.GetSubImage(2, 1) };
            Texture2D[] imgsRight = new Texture2D[3] { sheet.GetSubImage(0, 2), sheet.GetSubImage(1, 2), sheet.GetSubImage(2, 2) };

            animUp = new Animation(imgsUp);
            animDown = new Animation(imgsDown);
            animLeft = new Animation(imgsLeft);
            animRight = new Animation(imgsRight);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 newPos = Vector2.Zero;
            HandleInput(InputHandler.GamePadConnected, ref newPos);
            Move(Position.X + newPos.X, Position.Y + newPos.Y);
            Camera.SetPosition(Position - new Vector2(Camera.ViewWidth / 2, Camera.ViewHeight / 2));
        }

        private void HandleInput(bool gamePadConnected, ref Vector2 newPos)
        {
            if (gamePadConnected)
            {

            }
            else
            {
#if DEBUG
                if (InputHandler.KeyPressed(Keys.N)) noClip = !noClip;
#endif

                if (InputHandler.KeyDown(Keys.Up))
                    newPos.Y -= movementSpeed;
                if (InputHandler.KeyDown(Keys.Down))
                    newPos.Y += movementSpeed;
                if (InputHandler.KeyDown(Keys.Left))
                    newPos.X -= movementSpeed;
                if (InputHandler.KeyDown(Keys.Right))
                    newPos.X += movementSpeed;

                if (newPos.Y < 0) dir = 0;
                if (newPos.Y > 0) dir = 1;
                if (newPos.X < 0) dir = 2;
                if (newPos.X > 0) dir = 3;

                if (newPos == Vector2.Zero) 
                    isMoving = false;
                else 
                    isMoving = true;
            }
        }
    }
}
