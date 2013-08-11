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
        private string owner = "";
        private string text = "";
        private readonly int maxLines = 4;

        private Texture2D dialogueBoxTex;
        private SpriteFont dialogueFont;
        private SpriteFont dialogueBoldFont;

        private Rectangle dialogueBounds = new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT / 4);
        private Color semiTransparentGray;
        private float[] padding = new float[4] { 0, 0, 0, 0 };
        private bool multiPage = false;

        private string[] linesOfText;

        private int maxLettersPerLine;
        private int pages;
        private int currentPage = 1;
        private int charsPerPage;

        /// <summary>
        /// Gets the max letters per line
        /// </summary>
        public int MaxLettersPerLine
        {
            get { return maxLettersPerLine; }
            set
            {
                maxLettersPerLine = value;
                charsPerPage = maxLettersPerLine * maxLines;
            }
        }

        /// <summary>
        /// Gets the name of the owner of the dialogue
        /// </summary>
        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
            }
        }

        /// <summary>
        /// Gets the speech of the dialogue box
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                linesOfText = new string[1024];
                int charLength = value.Length;
                int lines = (int)Math.Round(((float)charLength / (float)maxLettersPerLine) + 0.5, 0);
                int leftOverChars = charLength % maxLettersPerLine;
                pages = lines;
                if (pages > 1)
                    multiPage = true;
                string tempValue = "";
                for (int i = 0; i < lines; i++)
                {
                    if (i == lines - 1)
                    {
                        tempValue += value.Substring(maxLettersPerLine * (lines - 1), leftOverChars);
                        linesOfText[i] = value.Substring(maxLettersPerLine * i, leftOverChars);
                    }
                    else
                    {
                        tempValue += value.Substring(maxLettersPerLine * i, maxLettersPerLine) + "\n";
                        linesOfText[i] = value.Substring(maxLettersPerLine * i, maxLettersPerLine);
                    }

                }
                text = tempValue;
            }
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
                MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString(" ").X);
            }
        }

        public float PaddingLeft
        {
            get { return padding[0]; }
            set
            {
                padding[0] = value;
                MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString(" ").X);
            }
        }

        public float PaddingTop
        {
            get { return padding[1]; }
            set
            {
                padding[1] = value;
                MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString("O").X);
            }
        }

        public float PaddingRight
        {
            get { return padding[2]; }
            set
            {
                padding[2] = value;
                MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString("O").X);
            }
        }

        public float PaddingBottom
        {
            get { return padding[3]; }
            set
            {
                padding[3] = value;
                MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString("O").X);
            }
        }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        public DialogueBox(HUD parent, string owner, string text)
        {
            this.parent = parent;
            this.owner = owner;
            this.text = text;

            dialogueBoxTex = new DrawableRectangle(parent.parentState.Game.GraphicsDevice, new Vector2(10, 10), Color.Black, true).Texture;
            dialogueFont = parent.parentState.Game.Content.Load<SpriteFont>("Fonts\\dialogue");
            dialogueBoldFont = parent.parentState.Game.Content.Load<SpriteFont>("Fonts\\dialogueBold");

            semiTransparentGray = new Color(.18f, .18f, .18f, .8f);

            MaxLettersPerLine = (int)((800 - PaddingLeft) / dialogueFont.MeasureString("O").X);
        }

        public void Update(GameTime gameTime)
        {
            if (multiPage)
            {
                if (InputHandler.KeyPressed(Keys.X))
                    currentPage += 1;

                if (currentPage > pages - maxLines)
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
            batch.DrawString(dialogueBoldFont, owner, new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop), Color.White);

            if (!multiPage)
            {
                for (int i = 0; i < maxLines; i++)
                {
                    batch.DrawString(dialogueFont, linesOfText[i] + "\n", new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop + dialogueFont.LineSpacing * i + dialogueBoldFont.LineSpacing), Color.White);
                }
            }
            else
            {
                for (int i = 0; i < maxLines; i++)
                {
                    batch.DrawString(dialogueFont, linesOfText[i + (currentPage)] + "\n", new Vector2(PaddingLeft, MainGame.GAME_HEIGHT - dialogueBounds.Height + PaddingTop + dialogueFont.LineSpacing * i + dialogueBoldFont.LineSpacing), Color.White);
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

            foreach (string word in words)
            {
                Vector2 size = dialogueFont.MeasureString(word);
                if (linewidth + size.X < 250)
                {
                    sb.Append(word + " ");
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    linewidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
    }
}
