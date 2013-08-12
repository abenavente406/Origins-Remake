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
using Origins_Remake.States;

namespace Origins_Remake.Entities.Mobs
{
    public class Npc : Entity
    {
        #region Fields

        /// <summary>
        /// How far an npc can detect a player
        /// </summary>
        public const int DETECT_RANGE = 30;

        /// <summary>
        /// How far an npc can be to talk to the player
        /// </summary>
        public const int TALK_RANGE = 5;

        private bool isBusy = false;

        private List<string> dialogues = new List<string>();

        /// <summary>
        /// A small list of possible random npc names
        /// </summary>
        private string[] randNamesMale = new string[]
        {
            "Marty",
            "Fred",
            "Edward",
            "Jordan",
            "Zaid",
            "Zachary",
            "Jacob",
            "Devin",
            "Daniel",
            "Geofferey",
            "Mohammed"
        };

        private Random rand = new Random();

        #endregion

        #region Properties

        /// <summary>
        /// All the dialogues the npc says
        /// </summary>
        public List<string> Dialogues
        {
            get { return dialogues; }
        }

        /// <summary>
        /// If the npc is able to interact with the player or not
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        #endregion

        #region Initialization
        public Npc(Vector2 pos, Random rand)
            : base(pos)
        {
            name = randNamesMale[rand.Next(randNamesMale.Length)];
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
            if (PlayerNear())
                dir = GetDirectionToPlayer();
        }


        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            base.Draw(batch, gameTime);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets if the player is nearby
        /// </summary>
        /// <returns>True/False</returns>
        private bool PlayerNear()
        {
            return Vector2.Distance(EntityManager.Player.Position, Position) < DETECT_RANGE;
        }

        /// <summary>
        /// Adds a dialogue sentence to the npc
        /// </summary>
        /// <param name="dialogue"></param>
        public void AddDialogue(string dialogue)
        {
            dialogues.Add(dialogue);
        }

        /// <summary>
        /// Gets a random dialogue from its list of dialogues
        /// </summary>
        /// <returns>A random dialogue</returns>
        public string GetRandomDialogue()
        {
            return dialogues.Count > 0 ? dialogues[new Random().Next(dialogues.Count)] : "I don't have any dialogues.";
        }

        /// <summary>
        /// Gets the angle to the player
        /// </summary>
        /// <param name="angleToPlayer"></param>
        private int GetDirectionToPlayer()
        {
            var angleToPlayer = EntityManager.GetAngleToPlayer(this);
            var dir = 0;

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

            return dir;
        }

        /// <summary>
        /// Speaks to the player
        /// </summary>
        public virtual void Speak()
        {
            IsBusy = true;
            GameplayState.hud.ShowDialogue(this);
        }


        public void SetTextures(Texture2D left = null, Texture2D right = null,
            Texture2D up = null, Texture2D down = null)
        {
            if (up != null)
                this.texUp = up;
            if (down != null)
                this.texDown = down;
            if (left != null)
                this.texLeft = left;
            if (right != null)
                this.texRight = right;
        }

        #endregion
    }
}
