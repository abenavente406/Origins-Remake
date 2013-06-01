using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;

namespace Origins_Remake.Levels
{
    class SlfLevel : Level
    {
        string path;
        bool editorMode = false;

        public SlfLevel(string path, bool editorMode = false)
            : base(0, 0, 0, 0)
        {
            this.path = path;

            if (!editorMode)
                LoadLevelFromFile(path);
        }

        private void LoadLevelFromFile(string path)
        {
            if (!File.Exists(path))
                return;

            FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read);

            BinaryFormatter serializer = new BinaryFormatter();
            Level loadedLevel = (Level) serializer.Deserialize(stream);

            this.realWidth = loadedLevel.RealWidth;
            this.realHeight = loadedLevel.RealHeight;
            this.tileWidth = loadedLevel.TileWidth;
            this.tileHeight = loadedLevel.TileHeight;
            this.map = loadedLevel.Map;

            stream.Close();
        }

        public override void Update(GameTime gameTime)
        {
            if (!editorMode)
                base.Update(gameTime);
            else
            {

            }
        }

        private void SaveLevel(string path)
        {
            FileStream stream = File.Create(path);

            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, this);

            stream.Close();
        }
    }
}
