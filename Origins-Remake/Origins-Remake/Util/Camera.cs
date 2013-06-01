using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Origins_Remake.Levels;

namespace Origins_Remake.Util
{
    public static class Camera
    {
        static MainGame gameRef;

        static Vector2 pos;
        static Vector2 minClamp;
        static Vector2 maxClamp;
        static int width;
        static int height;
        static float zoom;
        static Level displaylevel;

        public static Vector2 Position
        {
            get { return pos; }
            set { pos = Vector2.Clamp(value, minClamp, maxClamp); }
        }

        public static Rectangle ViewPortBounds
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, width, height); }
        }

        public static float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = MathHelper.Clamp(value, 0.1f, 10.0f);
                maxClamp = new Vector2(displaylevel.RealWidth - MainGame.GAME_WIDTH / zoom, displaylevel.RealHeight - MainGame.GAME_HEIGHT / zoom);
            }
        }

        public static void Initialize(Game game, Vector2 start, Level level)
        {
            gameRef = (MainGame)game;

            minClamp = new Vector2(0, 0);
            maxClamp = new Vector2(level.RealWidth - MainGame.GAME_WIDTH, level.RealHeight - MainGame.GAME_HEIGHT);
            displaylevel = level;
            Position = start;
            width = gameRef.GraphicsDevice.Viewport.Width;
            height = gameRef.GraphicsDevice.Viewport.Height;

            Zoom = 1.3f;
        }

        public static void Move(Vector2 amount)
        {
            Position += amount;
        }

        public static void SetPosition(Vector2 newPos)
        {
            Position = newPos;
        }

        public static Matrix GetTransformation()
        {
            var transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) * 
                            Matrix.CreateScale(new Vector3(Zoom, Zoom, 0));
            return transform;
        }

        public static bool IsOnCamera(Rectangle bounds)
        {
            return ViewPortBounds.Contains(bounds);
        }
    }
}
