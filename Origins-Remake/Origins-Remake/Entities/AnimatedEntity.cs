using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Entities
{
    public abstract class AnimatedEntity : Entity
    {
        protected Animation animUp;
        protected Animation animDown;
        protected Animation animLeft;
        protected Animation animRight;

        public AnimatedEntity(Vector2 pos)
            : base(pos) 
        {
            SetAnimation();
            SetTexture();
        }

        public abstract void SetAnimation();
        public abstract override void Update(GameTime gameTime);

        public override void SetTexture()
        {
            texUp = animUp.Images[0];
            texDown = animDown.Images[0];
            texLeft = animLeft.Images[0];
            texRight = animRight.Images[0];
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (isMoving)
            {
                switch (dir)
                {
                    case 0:
                        animUp.Draw(batch, gameTime, Position);
                        break;
                    case 1:
                        animDown.Draw(batch, gameTime, Position);
                        break;
                    case 2:
                        animLeft.Draw(batch, gameTime, Position);
                        break;
                    case 3:
                        animRight.Draw(batch, gameTime, Position);
                        break;
                }
            }
            else
            {
                base.Draw(batch, gameTime);
            }
        }
    }
}
