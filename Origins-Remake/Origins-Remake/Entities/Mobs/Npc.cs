using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using OriginsLib.Util;

namespace Origins_Remake.Entities.Mobs
{
    public class Npc : Entity
    {
        #region Fields
        protected List<string> dialogues = new List<string>();
        #endregion

        #region Properties
        public List<string> Dialogues
        {
            get { return dialogues; }
        }
        #endregion

        #region Initialization
        public Npc(Vector2 pos)
            : base(pos)
        {
        }

        public override void SetTexture()
        {
            return;
        }
        #endregion

        #region Update/Draw

        public override void Update(GameTime gameTime)
        {
            // TODO: Stay still until a player comes
            var angleToPlayer = EntityManager.GetAngleToPlayer(this);

            if (angleToPlayer <= Maths.PI_1_4 && angleToPlayer >= -Maths.PI_1_4)
            {
                dir = 3;
            }
            else if (angleToPlayer >= Maths.PI_1_4 && angleToPlayer <= Maths.PI_3_4)
            {
                dir = 0;
            }
            else if (angleToPlayer >= -Maths.PI_3_4 && angleToPlayer <= Maths.PI_3_4)
            {
                dir = 2;
            }
            else
            {
                dir = 1;
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            base.Draw(batch, gameTime);
        }

        #endregion

        #region Helper Methods
        protected bool PlayerNear()
        {
            return Vector2.Distance(EntityManager.Player.Position, Position) < 15;
        }
        #endregion
    }
}
