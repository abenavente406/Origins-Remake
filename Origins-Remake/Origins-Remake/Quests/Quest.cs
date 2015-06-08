using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Origins_Remake.Entities.Mobs;
using Microsoft.Xna.Framework;

namespace Origins_Remake.Quests
{
    public class Quest
    {

        private int questId;

        private QuestGiver questGiver;

        private Player player;

        public bool Active { get; set; }

        public Func<bool> CheckSuccess
        {
            get;
            set;
        }

        public int QuestId
        {
            get { return questId; }
            set { questId = value; }
        }

        public QuestGiver QuestGiver
        {
            get { return questGiver; }
            set { questGiver = value; }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Quest(int questId, QuestGiver questGiver, Player player)
        {
            this.questId = questId;
            this.questGiver = questGiver;
            this.player = player;
            this.Active = true;
        }

        public void Complete()
        {
            questGiver.CompleteQuest();
        }

        public void Update(GameTime gameTime)
        {
            if (CheckSuccess())
            {
                Complete();
            }
        }
    }
}
