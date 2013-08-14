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
using System.Reflection;
using System.Xml.Serialization;
using System.Diagnostics;

namespace OriginsLib.IO
{
    public static class IOManager
    {
        #region Fields

        private static string pathToDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Origins";
        private static string absolutePath = Path.Combine(pathToDir, @"save_");

        static XmlSerializer serializer;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path to the origins directory
        /// </summary>
        public static string PathToDir
        {
            get { return pathToDir; }
        }

        /// <summary>
        /// Gets the path to the origins save files
        /// </summary>
        public static string AbsolutePath
        {
            get { return absolutePath; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the io manager
        /// </summary>
        public static void Init()
        {
            // TODO: Put initialization logic here
            serializer = new XmlSerializer(typeof(EntityProperties));

            if (File.Exists(absolutePath))
            {
                File.Create(absolutePath);
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Saves the player's data
        /// </summary>
        /// <param name="properties"></param>
        public static bool SavePlayerData(EntityProperties properties)
        {
            try
            {
                var stream = File.Create(absolutePath + properties.Name + ".dat");
                serializer.Serialize(stream, properties);
                stream.Close();
                return true;
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Loads the data from a given player
        /// </summary>
        /// <param name="name">Name of the player</param>
        public static EntityProperties LoadPlayerData(string name)
        {
            EntityProperties results = new EntityProperties();
            try
            {
                if (File.Exists(absolutePath + name + ".dat"))
                {
                    // The load file was found --> continue
                    var stream = File.Open(absolutePath + name + ".dat", FileMode.Open);
                    results = (EntityProperties)serializer.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    throw new Exception("That load file was not found!\nFile: " + absolutePath + name + ".dat");
                }
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return results;
        }

        /// <summary>
        /// Gets all the save files from the directory
        /// </summary>
        /// <returns></returns>
        public static List<EntityProperties> GetAllPlayerDatas()
        {
            List<EntityProperties> results = new List<EntityProperties>();
            var files = Directory.GetFiles(pathToDir, "*.dat");

            foreach (string s in files)
            {
                var stream = File.Open(s, FileMode.Open);
                results.Add((EntityProperties)serializer.Deserialize(stream));
                stream.Close();
            }

            return results;
        }

        #endregion
    }
}
