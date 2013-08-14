using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Levels;

namespace Origins_Remake.Entities
{
    public abstract class Entity
    {
        #region Fields
        protected string name = "";
        protected Vector2 pos;
        protected bool isMoving = false;
        protected int dir = 0;
        protected int width;
        protected int height;
        protected float movementSpeed = 2.0f;

        protected Texture2D texUp;
        protected Texture2D texDown;
        protected Texture2D texLeft;
        protected Texture2D texRight;

        protected bool noClip = false;
        protected bool godMode = false;
        protected bool superSpeed = false;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Real world position of the entity
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return pos;
            }
            set
            {
                pos = Vector2.Clamp(value, Vector2.Zero, new Vector2(LevelManager.CurrentLevel.WidthInPixels - width,
                    LevelManager.CurrentLevel.HeightInPixels - height));
            }
        }

        /// <summary>
        /// Sprite width of the entity
        /// </summary>
        public int Width
        {
            get { return width; }
        }

        /// <summary>
        /// Sprite height of the entity
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        /// <summary>
        /// The direction the entity is facing
        /// </summary>
        public int Direction
        {
            get { return dir; }
        }

        /// <summary>
        /// Gets if the entity is in motion
        /// </summary>
        public bool IsMoving
        {
            get { return isMoving; }
        }

        /// <summary>
        /// Position of the player by tile
        /// </summary>
        public Point GridPosition
        {
            get
            {
                return LevelManager.Vector2Cell(Position);
            }
        }

        /// <summary>
        /// Gets the center of the entity
        /// </summary>
        public Vector2 Origin
        {
            get { return Position + new Vector2(width / 2, height / 2); }
        }

        /// <summary>
        /// Gets if the entity is invincible
        /// </summary>
        public bool GodMode
        {
            get { return godMode; }
        }

        /// <summary>
        /// Gets if the entity can ignore collisions
        /// </summary>
        public bool NoClip
        {
            get { return noClip; }
        }

        /// <summary>
        /// Gets if the entity can move extremely fasts
        /// </summary>
        public bool SuperSpeed
        {
            get { return superSpeed; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new entity
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Entity(Vector2 pos)
        {
            this.pos = pos;
        }

        public abstract void SetTexture();

        #endregion

        #region Update / Draw
        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            switch (dir)
            {
                case 0:
                    batch.Draw(texUp, Position, Color.White);
                    break;
                case 1:
                    batch.Draw(texDown, Position, Color.White);
                    break;
                case 2:
                    batch.Draw(texLeft, Position, Color.White);
                    break;
                case 3:
                    batch.Draw(texRight, Position, Color.White);
                    break;
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Moves the entity using a new position
        /// </summary>
        /// <param name="newPos"></param>
        protected void Move(Vector2 newPos)
        {
            if (!noClip)
            {
                // Check for collisions by testing each corner
                // -------------------------------------------
                var repPos = Position;

                // Find each corner of the player's bounds
                // ---------------------------------------
                var newPoint1 = Position + newPos;
                var newPoint2 = newPoint1 + new Vector2(0, height);
                var newPoint3 = newPoint1 + new Vector2(width, 0);
                var newPoint4 = newPoint1 + new Vector2(width, height);

                if (dir == 0 &&
                    !LevelManager.IsWallTile(newPoint1) &&
                    !LevelManager.IsWallTile(newPoint3))
                    repPos.Y += newPos.Y;

                if (dir == 1 &&
                    !LevelManager.IsWallTile(newPoint2) &&
                    !LevelManager.IsWallTile(newPoint4))
                    repPos.Y += newPos.Y;

                if (dir == 2 &&
                    !LevelManager.IsWallTile(newPoint1) &&
                    !LevelManager.IsWallTile(newPoint2))
                    repPos.X += newPos.X;

                if (dir == 3 &&
                    !LevelManager.IsWallTile(newPoint3) &&
                    !LevelManager.IsWallTile(newPoint4))
                    repPos.X += newPos.X;

                Position = repPos;
            }
            else
            {
                Position += newPos;
            }
        }

        /// <summary>
        /// Moves the player using x and y coords
        /// </summary>
        /// <param name="testX"></param>
        /// <param name="testY"></param>
        protected void Move(float testX, float testY)
        {
            if (!noClip)
            {
                if (!LevelManager.IsWallTile(testX, Position.Y, Width, height))
                    pos.X = testX;
                if (!LevelManager.IsWallTile(Position.X, testY, width, height))
                    pos.Y = testY;

                Position = pos;
            }
            else
                Position = new Vector2(testX, testY);
        }

        /// <summary>
        /// Gets the adjacent tile based on the direction the entity is facing
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        protected Point AdjacentPoint(Vector2 pos)
        {
            Point result = LevelManager.Vector2Cell(pos);
            switch (dir)
            {
                case 0:
                    result.Y--;
                    break;
                case 1:
                    result.Y++;
                    break;
                case 2:
                    result.X--;
                    break;
                case 3:
                    result.X++;
                    break;
            }
            return result;
        }
        #endregion
    }
}
