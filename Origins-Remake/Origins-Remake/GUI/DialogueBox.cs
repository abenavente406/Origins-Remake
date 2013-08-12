using System;
using GameHelperLibrary;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Origins_Remake.GUI
{

    /// <summary>
    /// Displays text to the screen said by an entity
    /// </summary>
    public class DialogueBox
    {
        private HUD parent;

        private string textOwner = "";
        private string text = "";
        private readonly int maxLinesInBox = 4;

        private Texture2D dialogueBoxTex;

        private SpriteFont dialogueFont;
        private SpriteFont dialogueBoldFont;

        private Rectangle dialogueBounds = new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT / 4);
        private Color semiTransparentGray;

        private float[] padding = new float[4] { 0, 0, 0, 0 };

        /// <summary>
        /// If the dialogue box has multiple pages
        /// </summary>
        private bool multiPage = false;

        /// <summary>
        /// The dialogue text split into lines
        /// </summary>
        private string[] linesOfText;

        private int currentLine = 1;
        private int numLines = 1;

        /// <summary>
        /// Gets the name of the owner of the dialogue
        /// </summary>
        public string TextOwner
        {
            get { return textOwner; }
            set { textOwner = value; }
        }

        /// <summary>
        /// Gets the speech of the dialogue box
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = WrapText(value); }
        }

        /// <summary>
        /// Gets the padding inside the box
        /// </summary>
        public float[] Padding
        {
            get { return padding; }
            set
            {
                padding = value;
            }
        }

        public float PaddingLeft
        {
            get { return padding[0]; }
            set
            {
                padding[0] = value;
            }
        }

        public float PaddingTop
        {
            get { return padding[1]; }
            set
            {
                padding[1] = value;
            }
        }

        public float PaddingRight
        {
            get { return padding[2]; }
            set
            {
                padding[2] = value;
            }
        }

        public float PaddingBottom
        {
            get { return padding[3]; }
            set
            {
                padding[3] = value;
            }
        }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        public int CurrentLine
        {
            get { return currentLine; }
            set { currentLine = value; }
        }

        public DialogueBox(HUD parent, string owner, string text)
        {
            this.parent = parent;
            this.textOwner = owner;
            this.text = text;

            dialogueBoxTex = new DrawableRectangle(parent.parentState.Game.GraphicsDevice, new Vector2(10, 10), Color.Black, true).Texture;
            dialogueFont = parent.parentState.Game.Content.Load<SpriteFont>("Fonts\\dialogue");
            dialogueBoldFont = parent.parentState.Game.Content.Load<SpriteFont>("Fonts\\dialogueBold");

            semiTransparentGray = new Color(.18f, .18f, .18f, .8f);
        }

        public void Update(GameTime gameTime)
        {
            if (multiPage)
            {
                if (InputHandler.KeyPressed(Keys.X))
                    currentLine += 1;

                if (currentLine > numLines - maxLinesInBox)
                    this.parent.ExitDialogue();
            }
            else
            {
                if (InputHandler.KeyPressed(Keys.X))
                    this.parent.ExitDialogue();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(dialogueBoxTex,
                        new Rectangle(0, MainGame.GAME_HEIGHT - dialogueBounds.Height, MainGame.GAME_WIDTH, dialogueBounds.Width),
                        semiTransparentGray);
            batch.DrawString(dialogueBoldFont, textOwner, new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop), Color.White);

            if (!multiPage)
            {
                for (int i = 0; i < numLines; i++)
                {
                    batch.DrawString(dialogueFont, linesOfText[i] + "\n", new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop + dialogueFont.LineSpacing * i + dialogueBoldFont.LineSpacing), Color.White);
                }
            }
            else
            {
                var counter = currentLine;
                for (int i = 0; i < maxLinesInBox; i++)
                {
                    if (counter < 0 || counter >= numLines)
                        continue;

                    batch.DrawString(dialogueFont, linesOfText[counter] + "\n", new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop + dialogueFont.LineSpacing * i + dialogueBoldFont.LineSpacing), Color.White);
                    counter++;
                }
            }

        }

        private string WrapText(string text)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float linewidth = 0f;
            float maxLine = MainGame.GAME_WIDTH - (PaddingLeft + PaddingRight); //a bit smaller than the box so you can have some padding...etc
            float spaceWidth = dialogueFont.MeasureString(" ").X;

            numLines = 1;

            foreach (string word in words)
            {
                Vector2 size = dialogueFont.MeasureString(word);
                if (linewidth + size.X < maxLine)
                {
                    sb.Append(word + " ");
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    numLines++;
                    sb.Append("\n" + word + " ");
                    linewidth = size.X + spaceWidth;
                }
            }

            linesOfText = new string[numLines];

            var split = sb.ToString().Split('\n');
            for (int i = 0; i < split.Length; i++)
                linesOfText[i] = split[i];

            multiPage = linesOfText.Length > maxLinesInBox;

            return sb.ToString();
        }
    }
}
