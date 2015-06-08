using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Origins_Remake.Quests
{
    public class QuestManager
    {

        private Dictionary<int, Quest> quests;

        public QuestManager()
        {
            quests = new Dictionary<int, Quest>();
        }

        public void AddQuest(Quest quest)
        {
            quests.Add(quest.QuestId, quest);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Quest q in quests.Values)
            {
                if (q.Active)
                {
                    q.Update(gameTime);
                }
            }
        }
    }
}
