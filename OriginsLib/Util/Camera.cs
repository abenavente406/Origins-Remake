using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OriginsLib.Util
{
    public static class Camera
    {
        #region Fields

        static Vector2 pos;
        static Vector2 startPos;
        static Vector2 maxClamp;
        static int viewWidth;
        static int viewHeight;
        static float zoom = 1.0f;

        #endregion

        #region Properties

        /// <summary>
        /// Position of the camera
        /// </summary>
        public static Vector2 Position
        {
            get { return pos; }
            set { pos = Vector2.Clamp(value, Vector2.Zero, MaxClamp); }
        }

        /// <summary>
        /// How far the camera can go
        /// </summary>
        public static Vector2 MaxClamp
        {
            get { return maxClamp; }
            set { maxClamp = Vector2.Clamp(value, Vector2.Zero, new Vector2(float.MaxValue, float.MaxValue)); }
        }

        /// <summary>
        /// How much the camera is zoomed in.  Closer to 0 = further away.
        /// </summary>
        public static float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.1f, 10f); }
        }

        /// <summary>
        /// The bounds of the camera's viewing area
        /// </summary>
        public static Rectangle View
        {
            get
            {
                return new Rectangle(
                            (int)(pos.X),
                            (int)(pos.Y),
                            (int)(viewWidth / Zoom),
                            (int)(viewHeight / Zoom));
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Intializes the camera
        /// </summary>
        /// <param name="startPos">Start position of the camera</param>
        /// <param name="maxClamp">Max position the camera can be at</param>
        public static void Initialize(Vector2 startPos, Rectangle view, Vector2 maxClamp)
        {
            MaxClamp = maxClamp - new Vector2(view.Width / 2, view.Height / 2);
            Position = Vector2.Zero;
            Camera.startPos = startPos;

            viewWidth = view.Width;
            viewHeight = view.Height;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Moves the camera by an amount
        /// </summary>
        /// <param name="amount"></param>
        public static void Move(Vector2 amount)
        {
            Position += amount;
        }

        /// <summary>
        /// Sets the position of the camera
        /// </summary>
        /// <param name="newPos">New position</param>
        public static void SetPosition(Vector2 newPos)
        {
            Position = newPos;
        }

        /// <summary>
        /// Gets the transformation matrix to shift the view
        /// </summary>
        /// <returns>Transformation matrix</returns>
        public static Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                   Matrix.CreateTranslation(new Vector3(startPos, 0)) *
                   Matrix.CreateScale(Zoom);
        }

        #endregion
    }
}
