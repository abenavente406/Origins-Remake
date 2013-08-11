using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Entities;
using Origins_Remake.Entities.Mobs;
using Origins_Remake.States;

namespace Origins_Remake.GUI
{
    public class HUD
    {
        private enum DrawingType { HUD, Dialogue, Inventory }

        public GameplayState parentState;
        private DrawingType drawingType = DrawingType.HUD;
        private DialogueBox dialogueBox;

        private Npc textOwner = null;

        public HUD(GameplayState parentState)
        {
            this.parentState = parentState;
            dialogueBox = new DialogueBox(this, "", "");
            dialogueBox.PaddingLeft = 128;
            dialogueBox.PaddingTop = 5f;
        }

        /// <summary>
        /// Shows and enters the dialgoue state
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="name"></param>
        /// <param name="dialogue"></param>
        public void ShowDialogue(Npc npc, string dialogue = "")
        {
            this.textOwner = npc;
            dialogueBox.CurrentPage = 1;

            parentState.currentState = GameplayState.State.Frozen;
            drawingType = DrawingType.Dialogue;
            dialogueBox.Owner = npc.Name;

            dialogueBox.Text = dialogue == "" ? npc.GetRandomDialogue() : dialogue;
        }

        /// <summary>
        /// Exits the dialogue state
        /// </summary>
        public void ExitDialogue()
        {
            this.textOwner.IsBusy = false;
            this.textOwner = null;

            EntityManager.Player.IsTalking = false;

            parentState.currentState = GameplayState.State.Playing;
            drawingType = DrawingType.HUD;
            dialogueBox.Owner = "";
            dialogueBox.Text = "";
        }

        public void Update(GameTime gameTime)
        {
            if (parentState.currentState == GameplayState.State.Frozen)
            {
                dialogueBox.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            switch (drawingType)
            {
                case DrawingType.HUD:
                    break;
                case DrawingType.Dialogue:
                    dialogueBox.Draw(gameTime, batch);
                    break;
                case DrawingType.Inventory:
                    break;
            }
        }
    }

}
