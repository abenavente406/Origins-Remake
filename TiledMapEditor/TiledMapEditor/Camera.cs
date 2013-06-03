using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TiledMapEditor
{
    class Camera
    {
        static Game1 gameRef;
        static Vector2 pos;

        public static Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public static void Initialize(Game game)
        {
            gameRef = (Game1)game;
            Position = Vector2.Zero;
        }

        public static void Move(Vector2 amount)
        {
            Position += amount;
        }

        public static Matrix GetMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0));
        }
    }
}
