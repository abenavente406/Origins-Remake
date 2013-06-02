using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using TileEngineLibrary.Tiles;

namespace TileEngineLibrary
{
    [Serializable]
    public class SlfLevel : Level
    {
        string path;
        bool editorMode = false;

        public SlfLevel(string path)
            : base(0, 0, 1, 1)
        {
            this.path = path;

            if (this.path != null)
                LoadLevelFromFile(path);
        }

        public void LoadLevelFromFile(string path)
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
            foreach (Tile t in this.map)
            {
                if (t.GetType() == typeof(AnimatedTile))
                    (t as AnimatedTile).SetTileAnimation();
                t.SetTileSolid();
                t.SetTileTexture();
            }

            stream.Close();
        }

        public void SaveLevel(string path)
        {
            FileStream stream = File.Create(path);

            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, this);

            stream.Close();
        }

        public void SetTile(Point p, Tile t)
        {
            map[p.X, p.Y] = t;
        }

        public override void Update(GameTime gameTime)
        {
            if (!editorMode)
                base.Update(gameTime);
            else
            {

            }
        }
    }
}
