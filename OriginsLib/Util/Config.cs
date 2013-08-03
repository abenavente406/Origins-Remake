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
using System.IO;

namespace OriginsLib.Util
{
    public static class Config
    {
        #region Fields
        static Game gameRef;
        static ContentManager content;

        public static int GAME_WIDTH = 800;
        public static int GAME_HEIGHT = 480;
        public static bool FULL_SCREEN = false;
        public static float SCALE = 1.0f;
        public static string lastLogin;

        static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\";
        #endregion

        #region Properties
        /// <summary>
        /// Global graphics device for the game
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return gameRef.GraphicsDevice; }
        }

        /// <summary>
        /// Global game reference
        /// </summary>
        public static Game GameRef
        {
            get { return gameRef; }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the globals
        /// </summary>
        /// <param name="gameRef"></param>
        public static void Initialize(Game gameRef)
        {
            Config.gameRef = gameRef;
            Config.content = gameRef.Content;
        }
        #endregion

        #region Helper Methods

        public static void SetLastLogin(string name)
        {
            lastLogin = name;

            var fs = File.Create(Path.Combine(filePath, @"last_login"));
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(name);
            writer.WriteLine("Time|" + DateTime.Now.ToString());
            writer.Close();
            fs.Close();
            fs.Dispose();
        }

        public static void CheckLastLogin()
        {
            if (File.Exists(Path.Combine(filePath, @"last_login")))
            {
                var fs = File.Open(Path.Combine(filePath, @"last_login"), FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(fs);
                lastLogin = reader.ReadLine();
                reader.Close();
                reader.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }

        #endregion
    }
}
