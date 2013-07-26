using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OriginsLibrary.Util
{
    public static class Camera
    {
        private static Vector2 pos;
        private static Vector2 upperClamp = Vector2.Zero;
        private static int viewWidth;
        private static int viewHeight;
        private static float zoom = 1.0f;

        public static Vector2 Position
        {
            get { return pos; }
            set { pos = Vector2.Clamp(value, Vector2.Zero, upperClamp - new Vector2(ViewWidth, ViewHeight)); }
        }

        public static int ViewWidth
        {
            get { return (int)(viewWidth / zoom); }
        }

        public static int ViewHeight
        {
            get { return (int)(viewHeight / zoom); }
        }

        public static float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.05f, 10.0f); }
        }

        public static void Initialize(int viewWidth, int viewHeight, int xClamp, int yClamp)
        {
            Camera.viewWidth = viewWidth;
            Camera.viewHeight = viewHeight;
            upperClamp = new Vector2(xClamp, yClamp);
        }

        public static void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public static Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                  Matrix.CreateScale(new Vector3(zoom, zoom, 1));
        }
    }
}
