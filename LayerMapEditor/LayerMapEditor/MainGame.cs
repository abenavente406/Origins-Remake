using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;

namespace LayerMapEditor
{
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public const int WINDOW_WIDTH = 1366;
        public const int WINDOW_HEIGHT = 768;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Canvas canvas;

        private Texture2D testTexture;
        private Color controlColor;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));

            canvas = new Canvas(this);
            canvas.Initialize();

            Camera.Initialize(WINDOW_WIDTH, WINDOW_HEIGHT, 100000, 100000);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testTexture = CreateBG(WINDOW_WIDTH, WINDOW_HEIGHT);
            canvas.LoadContent();

            var color = 210;
            controlColor = new Color(color, color, color);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (canvas.Bounds.Contains(InputHandler.MousePos))
            {
                // Handle stuff in the canvas
                canvas.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(controlColor);

            spriteBatch.Begin();
            spriteBatch.Draw(testTexture, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
            spriteBatch.End();

            canvas.Draw(spriteBatch, gameTime);
            
            base.Draw(gameTime);
        }

        private Texture2D CreateBG(int width, int height)
        {
            var backgroundTex = new Texture2D(graphics.GraphicsDevice, width, height);

            Color[] bgc = new Color[height * width];
            int texColour = -5;          // Defines the colour of the gradient.
            int gradientThickness = 16;  // Defines how "diluted" the gradient gets. I've found 2 works great, and 16 is a very fine gradient.

            for (int i = 0; i < bgc.Length; i++)
            {
                texColour = (i / (width * gradientThickness));
                bgc[i] = new Color(texColour, texColour, texColour, 0);
            }
            backgroundTex.SetData(bgc);
            return backgroundTex;
        }
    }
}
