using GameHelperLibrary;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLib.TileEngine;

namespace LayerMapEditor
{
    public class TileSelector
    {
        #region Fields
        private TileMap levelRef;
        private MainGame gameRef;
        private int selectedIndex = 0;
        private Texture2D loadedTileSet;
        private int width;
        private Vector2 startPos;

        /// <summary>
        /// Used to show which tile is selected
        /// </summary>
        private Texture2D plainWhite;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the x coordinate of the current tile
        /// </summary>
        public int SelectedX
        {
            get { return gameRef.level.CurrentTileSet.SourceRectangles[SelectedIndex].X / Engine.TileWidth; }
        }

        /// <summary>
        /// Gets the y coordinate of the current tile
        /// </summary>
        public int SelectedY
        {
            get { return gameRef.level.CurrentTileSet.SourceRectangles[SelectedIndex].Y / Engine.TileHeight; }
        }

        /// <summary>
        /// Returns the current selected index of the tile
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        /// <summary>
        /// The width of the selector
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// The height of the selector
        /// </summary>
        public int Height
        {
            get { return (int)(LoadedImage.Height / TileScale); }
        }

        /// <summary>
        /// Gets how much the height should be scaled
        /// </summary>
        public float TileScale
        {
            get { return loadedTileSet.Width / Width; }
        }

        /// <summary>
        /// Bounds of the selector --> used for finding out when to update
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)startPos.X, (int)startPos.Y, width, Height); }
        }

        /// <summary>
        /// Gets the image for the current tile set
        /// </summary>
        public Texture2D LoadedImage
        {
            get { return levelRef.CurrentTileSet.Image; }
        }

        #endregion

        #region Initialization

        public TileSelector(MainGame game, Vector2 startPos)
        {
            gameRef = game;
            levelRef = game.level;
            selectedIndex = gameRef.SelectedTileIndex;
            this.startPos = startPos;

            if (gameRef.level.CurrentTileSet != null)
                loadedTileSet = gameRef.level.CurrentTileSet.Image;

            plainWhite = new DrawableRectangle(gameRef.GraphicsDevice, Vector2.One, Color.White, true).Texture;
        }
        #endregion

        #region Update/Draw

        /// <summary>
        /// Update the tile selector
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            var transformedMousePos = new Vector2(InputHandler.MousePos.X,
                InputHandler.MousePos.Y) - startPos;
            var transformedTilePos = new Vector2((int)(transformedMousePos.X / Engine.TileWidth),
                (int)(transformedMousePos.Y / Engine.TileHeight));

            if (InputHandler.MouseButtonDown(MouseButton.LeftButton))
            {
                var i = (int)((transformedTilePos.Y * levelRef.CurrentTileSet.WidthInTiles) + transformedTilePos.X);
                gameRef.SelectedTileIndex = i;
                gameRef.hud.SelectedTileIndex = i;
                SelectedIndex = i;
            }
        }

        /// <summary>
        /// Draw the tile selector
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(loadedTileSet, new Rectangle(
                (int)startPos.X, (int)startPos.Y, Width, Height),
                Color.White);

            batch.Draw(plainWhite, 
                new Rectangle((int)(SelectedX * (Engine.TileWidth / TileScale) + (int)startPos.X), (int)(SelectedY * (Engine.TileWidth / TileScale) + (int)startPos.Y), 
                    (int)(Engine.TileWidth / TileScale), (int)(Engine.TileHeight / TileScale)),
                Color.Red * .5f);
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
