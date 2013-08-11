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

namespace Origins_Remake.Entities.Mobs
{
    public class QuestGiver : Npc
    {
        #region Events

        /// <summary>
        /// Executes whenever the player completes the quest
        /// </summary>
        public event EventHandler OnQuestCompleted;

        /// <summary>
        /// Executes whenever the player speaks to the quest giver for the first time
        /// </summary>
        public event EventHandler OnFirstTimeSpokenTo;

        #endregion

        #region Fields

        private int questId = 0;
        private bool questCompleted = false;
        private bool firstTimeSpokenTo = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the quest id for the quest giver
        /// </summary>
        public int QuestId
        {
            get { return questId; }
        }

        /// <summary>
        /// Gets if the quest is completed
        /// </summary>
        public bool QuestCompleted
        {
            get { return questCompleted; }
        }

        #endregion

        #region Initialization

        public QuestGiver(Vector2 pos)
            : base(pos, new Random()) { }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Completes the quest
        /// </summary>
        public void CompleteQuest()
        {
            questCompleted = true;
            OnQuestCompleted(this, null);
        }

        /// <summary>
        /// Speaks to the player
        /// </summary>
        public override void Speak()
        {
            if (!firstTimeSpokenTo)
            {
                if (OnFirstTimeSpokenTo != null)
                    OnFirstTimeSpokenTo(this, null);
                firstTimeSpokenTo = true;
            }


            // TODO: Speak to the player by changing the game state and 
            //       popping up a black box with text
            // --------------------------------------------------------
        }

        #endregion
    }
}
